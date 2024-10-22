using Microsoft.Extensions.Logging;

namespace Test.Work.BooksCatalogApi.Database
{
    /// <summary>
	/// Конфиг проекта
	/// </summary>
	public class PostgresDbOptions
    {
        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public string? ConnectionString { get; set; } = default!;

        /// <summary>
        /// Фабрика логгера для команд SQL
        /// </summary>
        public ILoggerFactory? SqlLoggerFactory { get; set; }
    }
}
