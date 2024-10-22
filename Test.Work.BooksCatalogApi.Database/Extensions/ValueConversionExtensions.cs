using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Test.Work.BooksCatalogApi.Database.Extensions
{
    /// <summary>
	/// Расширение преобразования значений типа jsonb
	/// </summary>
	public static class ValueConversionExtensions
    {
        /// <summary>
        /// Установить тип и преобразование
        /// </summary>
        /// <typeparam name="T">Десериализованный тип</typeparam>
        /// <param name="propertyBuilder">Строитель</param>
        /// <returns>Строитель</returns>
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder)
            where T : class?, new()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>()
            };
            var converter = new ValueConverter<T, string>(
                v => JsonConvert.SerializeObject(v, settings),
                v => JsonConvert.DeserializeObject<T>(v, settings)!);

            propertyBuilder.HasConversion(converter);
            propertyBuilder.HasColumnType("jsonb");

            return propertyBuilder;
        }
    }
}
