using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Entities.Common;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Database.Extensions;

namespace Test.Work.BooksCatalogApi.Database
{
    /// <summary>
	/// Контекст EF Core для приложения
	/// </summary>
	public class EfContext : DbContext, IDbContext
    {
        private const string DefaultSchema = "public";
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMediator _domainEventsDispatcher;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">Параметры подключения к БД</param>
        /// <param name="userContext">Контекст текущего пользователя</param>
        /// <param name="dateTimeProvider">Провайдер даты и времени</param>
        /// <param name="domainEventsDispatcher">Медиатор для доменных событий</param>
        public EfContext(
            DbContextOptions<EfContext> options,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IMediator domainEventsDispatcher)
            : base(options)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <inheritdoc/>
        public DbSet<Book> Books { get; set; }

        /// <inheritdoc/>
        public DbSet<Author> Authors { get; set; }

        /// <inheritdoc/>
        public DbSet<CoAuthor> CoAuthors { get; set; }

        /// <inheritdoc/>
        public bool IsInMemory => Database.IsInMemory();

        /// <inheritdoc/>
        public void Clean()
        {
            var changedEntriesCopy = ChangeTracker.Entries()
                .Where(e => e.State is EntityState.Added or
                            EntityState.Modified or
                            EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        /// <inheritdoc/>
        public override int SaveChanges()
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
            => SaveChangesAsync(true, default).GetAwaiter().GetResult();
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntries = ChangeTracker.Entries().ToArray();

            // перед применением событий получаем их все из доменных сущностей во избежание дубликации в рекурсии
            var domainEvents = entityEntries
                .Select(po => po.Entity)
                .OfType<EntityBase>()
                .SelectMany(x => x.RetrieveDomainEvents())
                .ToArray();

            try
            {
                foreach (var @event in domainEvents)
                    await _domainEventsDispatcher.Publish(@event, cancellationToken);

                if (entityEntries.Length > 10)
                    entityEntries.AsParallel().ForAll(OnSave);
                else
                    foreach (var entityEntry in entityEntries)
                        OnSave(entityEntry);

                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                return HandleDbUpdateException(ex, cancellationToken);
            }
        }

        /// <inheritdoc/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await SaveChangesAsync(true, cancellationToken);

        protected virtual int HandleDbUpdateException(DbUpdateException ex, CancellationToken cancellationToken = default)
        {
            if (ex?.InnerException is PostgresException postgresEx)
                throw postgresEx.SqlState switch
                {
                    PostgresErrorCodes.ForeignKeyViolation => new ApplicationExceptionBase(
                        $"Заданы некорректные идентификаторы для внешних ключей сущности: {postgresEx.Detail}", ex),
                    PostgresErrorCodes.UniqueViolation => new DuplicateUniqueKeyException(ex),
                    _ => ex,
                };
            throw ex ?? throw new ArgumentNullException(nameof(ex));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Глобальные фильтры на soft delete
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                    entityType.AddSoftDeleteQueryFilter();

                if (entityType.IsKeyless)
                {
                    continue;
                }
            }
        }

        private void SoftDeleted(EntityEntry entityEntry)
        {
            if (entityEntry?.Entity is not null
                && entityEntry.Entity is ISoftDeletable softDeleteable)
            {
                softDeleteable.DeletedAt = _dateTimeProvider.UtcNow;
                entityEntry.State = EntityState.Modified;
            }
        }

        private void OnSave(EntityEntry entityEntry)
        {
            // TODO: вынести в домен
            if (entityEntry.State != EntityState.Unchanged)
            {
                UpdateTimestamp(entityEntry);
                SetModifiedUser(entityEntry);
            }

            if (entityEntry.State == EntityState.Deleted)
                SoftDeleted(entityEntry);
        }

        private void UpdateTimestamp(EntityEntry entityEntry)
        {
            var entity = entityEntry.Entity;
            if (entity is null)
                return;

            // TODO: лучше бы вызывать функцию бд now() at time zone 'utc', но не нашел как адекватно вмешаться в апдейт
            if (entity is ITimeTrackable table)
            {
                table.UpdatedAt = _dateTimeProvider.UtcNow;
                if (entityEntry.State == EntityState.Added)
                    table.CreatedAt = _dateTimeProvider.UtcNow;
            }
        }

        private void SetModifiedUser(EntityEntry entityEntry)
        {
            if (entityEntry?.Entity != null
                && entityEntry.State != EntityState.Unchanged
                && entityEntry.Entity is IUserTrackable userTrackable)
            {
                userTrackable.ModifiedByUserId = _userContext.CurrentUserId;

                if (entityEntry.State == EntityState.Added)
                    userTrackable.CreatedByUserId = _userContext.CurrentUserId;
            }
        }
    }
}
