using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Test.Work.BooksCatalogApi.BLL.Entities.Common;

namespace Test.Work.BooksCatalogApi.Database.Extensions
{
    /// <summary>
	/// Методы расширения для конфигурации сущностей
	/// </summary>
	public static class PropertyBuilderExtensions
    {
        private const string NowCommand = "now()";

        /// <summary>
        /// Конфигурация поля CreatedOn
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="builder">Билдер</param>
        public static PropertyBuilder<DateTime> ConfigureCreatedOn<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IAddTrackable
            => builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasComment("Время создания записи")
                .HasDefaultValueSql(NowCommand);

        /// <summary>
        /// Конфигурация поля ModifiedOn
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="builder">Билдер</param>
        public static PropertyBuilder<DateTime> ConfigureModifiedOn<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IUpdateTrackable
            => builder.Property(x => x.UpdatedAt)
                .HasComment("Время изменения записи")
                .IsRequired();

        /// <summary>
        /// Конфигурация полей CreatedOn и ModifiedOn
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="builder">Билдер</param>
        public static void ConfigureTimeTrackableEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, ITimeTrackable
        {
            builder.ConfigureCreatedOn();
            builder.ConfigureModifiedOn();
        }
    }
}
