using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;
using Test.Work.BooksCatalogApi.Options;

namespace Test.Work.BooksCatalogApi.ResponseCompression
{
    /// <summary>
	/// Точка входа опций сжатия ответов для приложения
	/// </summary>
	public static class Entry
    {
        /// <summary>
        /// Добавить сжатие ответа.
        /// Оно отключено для HTTPS во избежание BREACH уязвимости
        /// </summary>
        /// <param name="services">Коллекция служб приложения</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>Коллекция служб приложения со сжатием</returns>
        public static IServiceCollection AddCustomResponseCompression(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
                .Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal)
                .AddResponseCompression(
                    options =>
                    {
                        // Разрешить кастомным MIME-types GZIP
                        var customMimeTypes = configuration
                            .GetSection(nameof(GlobalOptions.Compression))
                            .Get<CompressionOptions>()
                            ?.MimeTypes ?? Enumerable.Empty<string>();
                        options.MimeTypes = customMimeTypes.Concat(ResponseCompressionDefaults.MimeTypes);

                        options.Providers.Add<BrotliCompressionProvider>();
                        options.Providers.Add<GzipCompressionProvider>();
                    });
    }
}
