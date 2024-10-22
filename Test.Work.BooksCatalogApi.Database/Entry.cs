using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Test.Work.BooksCatalogApi.BLL.Abstractions;

namespace Test.Work.BooksCatalogApi.Database
{
    /// <summary>
	/// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
	/// </summary>
	public static class Entry
    {
        /// <summary>
        /// Добавить службы проекта с Postgresql
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <param name="optionsAction">Параметры подключения к Postgresql</param>
        /// <returns>Обновленная коллекция служб</returns>
        public static IServiceCollection AddPostgreSql(
            this IServiceCollection services,
            Action<PostgresDbOptions>? optionsAction)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var options = new PostgresDbOptions();
            optionsAction?.Invoke(options);

            return services.AddPostgreSql(options);
        }

        /// <summary>
        /// Добавить службы проекта с Postgresql
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <param name="options">Конфиг подключения к Postgresql</param>
        /// <returns>Обновленная коллекция служб</returns>
        public static IServiceCollection AddPostgreSql(
            this IServiceCollection services,
            PostgresDbOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.ConnectionString))
                throw new ArgumentException(nameof(options.ConnectionString));

            services.AddDbContext<EfContext>(opt =>
            {
                if (options.SqlLoggerFactory != null)
                    opt.UseLoggerFactory(options.SqlLoggerFactory);
                opt.UseSnakeCaseNamingConvention();
                opt.UseNpgsql(options.ConnectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

            services.AddTransient<DbMigrator>();
            services.AddScoped<IDbContext>(x => x.GetRequiredService<EfContext>());

            return services;
        }

        /// <summary>
        /// Добавить healthcheck на <see cref="EfContext"/>
        /// </summary>
        /// <param name="healthChecksBuilder">Строитель healthcheck'ов</param>
        /// <param name="name">Назвнаие</param>
        /// <param name="failureStatus">Статус ошибки</param>
        /// <param name="tags">Тэги</param>
        /// <returns>Строитель healthcheck-ов</returns>
        public static IHealthChecksBuilder AddPostgreSql(
            this IHealthChecksBuilder healthChecksBuilder,
            string? name = null,
            HealthStatus? failureStatus = null,
            IEnumerable<string>? tags = null)
                => healthChecksBuilder.AddDbContextCheck<EfContext>(name, failureStatus, tags);
    }
}
