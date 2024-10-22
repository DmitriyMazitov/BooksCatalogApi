using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Test.Work.BooksCatalogApi.Cors;
using Test.Work.BooksCatalogApi.Database;

namespace Test.Work.BooksCatalogApi.HealthChecks
{
    /// <summary>
	/// Точка входа сервисов HealthCheck-ов для приложения
	/// </summary>
	public static class Entry
    {
        /// <summary>
        /// Добавить службы HealthCheck-ов
        /// </summary>
        /// <param name="services">Коллекция служб приложения</param>
        /// <returns>Коллекция служб приложения с HealthChecks</returns>
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services) =>
            services
                .AddHealthChecks()
                .AddPostgreSql("postgresql", HealthStatus.Unhealthy, new[] { "external", "db" })
                .Services;

        /// <summary>
        /// Создать эндпоинты для readiness probe и liveness probe
        /// </summary>
        /// <param name="builder">Строитель пайплайна приложения</param>
        /// <param name="readinessProbeUrl">URL readiness probe</param>
        /// <param name="livenessProbeUrl">URL liveness probe</param>
        /// <returns>Строитель пайплайна приложения с readiness probe и liveness probe</returns>
        public static IEndpointRouteBuilder AddHealthCheckEndpoints(
            this IEndpointRouteBuilder builder,
            string readinessProbeUrl = "/health",
            string livenessProbeUrl = "/health/live")
        {
            builder
                .MapHealthChecks(readinessProbeUrl, new HealthCheckOptions
                {
                    ResponseWriter = WriteResponseAsync,
                })
                .RequireCors(CorsPolicies.AllowAny);
            builder
                .MapHealthChecks(livenessProbeUrl, new HealthCheckOptions
                {
                    ResponseWriter = WriteResponseAsync,
                    Predicate = _ => false,
                })
                .RequireCors(CorsPolicies.AllowAny);
            return builder;
        }

        private static Task WriteResponseAsync(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions
            {
                Indented = true,
            };

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream, options))
                {
                    writer.WriteStartObject();
                    writer.WriteString("status", result.Status.ToString());
                    writer.WriteStartObject("results");
                    foreach (var entry in result.Entries)
                    {
                        writer.WriteStartObject(entry.Key);
                        writer.WriteString("status", entry.Value.Status.ToString());
                        writer.WriteString("description", entry.Value.Description);
                        writer.WriteStartObject("data");
                        foreach (var item in entry.Value.Data)
                        {
                            writer.WritePropertyName(item.Key);
                            JsonSerializer.Serialize(
                                writer, item.Value, item.Value.GetType());
                        }
                        writer.WriteEndObject();
                        writer.WriteEndObject();
                    }
                    writer.WriteEndObject();
                    writer.WriteEndObject();
                }

                var json = Encoding.UTF8.GetString(stream.ToArray());

                return context.Response.WriteAsync(json);
            }
        }
    }
}
