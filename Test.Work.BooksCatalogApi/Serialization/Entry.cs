using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Test.Work.BooksCatalogApi.Options;

namespace Test.Work.BooksCatalogApi.Serialization
{
    /// <summary>
	/// Точка входа сервисов логирования для приложения
	/// </summary>
	public static class Entry
    {
        /// <summary>
        /// Сконфигурировать службы сериализации ASP.NET Core
        /// </summary>
        /// <param name="builder">Строитель контекста MVC</param>
        /// <param name="webHostEnvironment">Окружение сервиса</param>
        /// <returns>Строитель контекста MVC с параметрами json</returns>
        public static IMvcBuilder AddCustomJsonOptions(
            this IMvcBuilder builder,
            IWebHostEnvironment webHostEnvironment) =>
            builder.AddJsonOptions(
                options =>
                {
                    var jsonSerializerOptions = options.JsonSerializerOptions;
                    if (webHostEnvironment.IsDevelopment())
                    {
                        // Pretty print the JSON in development for easier debugging.
                        jsonSerializerOptions.WriteIndented = true;
                    }

                    jsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.Cyrillic, UnicodeRanges.BasicLatin);
                    jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    jsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });

        /// <summary>
        /// Применить параметры сериализации MVC
        /// </summary>
        /// <param name="builder">Строитель MVC</param>
        /// <param name="configuration">Конфиг</param>
        /// <returns>Строитель с параметрами сериализации MVC</returns>
        public static IMvcBuilder AddCustomMvcOptions(this IMvcBuilder builder, IConfiguration configuration) =>
            builder.AddMvcOptions(
                options =>
                {
                    var cacheProfileOptions = configuration
                        .GetSection(nameof(GlobalOptions.CacheProfiles))
                        .Get<CacheProfileOptions>();

                    if (cacheProfileOptions != null)
                        foreach (var keyValuePair in cacheProfileOptions)
                            options.CacheProfiles.Add(keyValuePair);

                    // возвращает 406 Not Acceptable если MIME-type в заголовке Accept некорректный.
                    // options.ReturnHttpNotAcceptable = true;
                });
    }
}
