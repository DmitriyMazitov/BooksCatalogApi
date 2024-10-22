using Microsoft.AspNetCore.Mvc.Infrastructure;
using Test.Work.BooksCatalogApi.Authentication;
using Test.Work.BooksCatalogApi.BLL;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Services;
using Test.Work.BooksCatalogApi.Cors;
using Test.Work.BooksCatalogApi.Database;
using Test.Work.BooksCatalogApi.HealthChecks;
using Test.Work.BooksCatalogApi.HttpCache;
using Test.Work.BooksCatalogApi.Logging;
using Test.Work.BooksCatalogApi.Options;
using Test.Work.BooksCatalogApi.ResponseCompression;
using Test.Work.BooksCatalogApi.Serialization;
using Test.Work.BooksCatalogApi.Swagger;
using Test.Work.BooksCatalogApi.Versioning;

namespace Test.Work.BooksCatalogApi
{
    /// <summary>
	/// Стартап веб-сервиса
	/// </summary>
	public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="configuration">Конфигурация</param>
        /// <param name="webHostEnvironment">Окружение и его переменные</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Конфигурация служб и зависимостей ASP.NET Core
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        /// <param name="services">Службы</param>
        public virtual void ConfigureServices(IServiceCollection services) =>
            services
                .AddCustomCaching()
                .AddCustomCors()
                .AddCustomOptions(_configuration)
                .AddRouting()
                .AddResponseCaching()
                .AddCustomResponseCompression(_configuration)
                .AddCustomHealthChecks()
                .AddCustomSwagger()
                .AddHttpContextAccessor()
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                .AddSingleton<IDbSeeder, DbSeeder>()
                .AddCustomApiVersioning()
                .AddControllers()
                    .AddCustomJsonOptions(_webHostEnvironment)
                .AddCustomMvcOptions(_configuration)
                .Services
                .AddUserContext()
                .AddPostgreSql(x =>
                {
                    x.ConnectionString = _configuration["Application:DbConnectionString"];
                    x.SqlLoggerFactory = _webHostEnvironment.IsDevelopment()
                        ? LoggerFactory.Create(builder => builder.AddConsole())
                        : null;
                })
                .AddBll()
                .AddCustomHeaderAuthentication(services);

        /// <summary>
        /// Конфигурация пайплайна обработки запроса ASP.NET Core
        /// </summary>
        /// <param name="application">Билдер приложения</param>
        public virtual void Configure(IApplicationBuilder application) =>
            application
                .UseForwardedHeaders()
                .UseRouting()
                .UseCors(CorsPolicies.AllowAny)
                .UseResponseCaching()
                .UseResponseCompression()
                .UseStaticFilesWithCacheControl()
                .UseCustomSerilogRequestLogging()
                .UseExceptionHandling()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(
                    builder =>
                    {
                        builder.MapControllers().RequireCors(CorsPolicies.AllowAny);
                        builder.AddHealthCheckEndpoints();
                    })
                .UseCustomSwagger();
    }
}
