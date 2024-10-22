using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using Test.Work.BooksCatalogApi.BLL.Models;
using Test.Work.BooksCatalogApi.Extensions;

namespace Test.Work.BooksCatalogApi.Options
{
    /// <summary>
	/// Точка входа опций конфигурации для приложения
	/// </summary>
	public static class Entry
    {
        /// <summary>
        /// Configures the settings by binding the contents of the appsettings.json file to the specified Plain Old CLR
        /// Objects (POCO) and adding <see cref="IOptions{TOptions}"/> objects to the services collection.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>The services with options services added.</returns>
        public static IServiceCollection AddCustomOptions(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .ConfigureAndValidateSingleton<GlobalOptions>(configuration)
                .ConfigureAndValidateSingleton<CompressionOptions>(configuration.GetSection(nameof(GlobalOptions.Compression)))
                .ConfigureAndValidateSingleton<ForwardedHeadersOptions>(configuration.GetSection(nameof(GlobalOptions.ForwardedHeaders)))
                .Configure<ForwardedHeadersOptions>(
                    options =>
                    {
                        options.KnownNetworks.Clear();
                        options.KnownProxies.Clear();
                    })
                .ConfigureAndValidateSingleton<ApplicationOptions>(configuration.GetSection(nameof(GlobalOptions.Application)))
                .ConfigureAndValidateSingleton<AuthenticationTokenOptions>(configuration.GetSection(nameof(GlobalOptions.Token)))
                .ConfigureAndValidateSingleton<CacheProfileOptions>(configuration.GetSection(nameof(GlobalOptions.CacheProfiles)))
                .ConfigureAndValidateSingleton<KestrelServerOptions>(configuration.GetSection(nameof(GlobalOptions.Kestrel)));
    }
}
