using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Work.BooksCatalogApi.BLL.Abstractions;

namespace Test.Work.BooksCatalogApi.Database
{
    /// <summary>
	/// Мигратор
	/// </summary>
	public class DbMigrator
    {
        private readonly EfContext _documentDbContext;
        private readonly ILogger<DbMigrator> _logger;
        private readonly IDbSeeder _dbSeeder;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="logger">Логгер</param>
        /// <param name="dbSeeder">Сервис добавления записей в БД</param>
        /// <param name="dateTimeProvider">Провайдер даты</param>
        public DbMigrator(
            EfContext dbContext,
            ILogger<DbMigrator> logger,
            IDbSeeder dbSeeder,
            IDateTimeProvider dateTimeProvider)
        {
            _documentDbContext = dbContext;
            _logger = logger;
            _dbSeeder = dbSeeder;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Мигрировать БД
        /// </summary>
        /// <returns>Ничего</returns>
        public async Task MigrateAsync()
        {
            var operationId = Guid.NewGuid().ToString().Substring(0, 4);
            _logger.LogInformation($"UpdateDatabase:{operationId}: starting...");
            try
            {
                await _documentDbContext.Database.MigrateAsync();
                await _dbSeeder.SeedAsync(_documentDbContext);
                _logger.LogInformation($"UpdateDatabase:{operationId}: successfully done");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"UpdateDatabase:{operationId}: failed");
                throw;
            }
        }
    }
}
