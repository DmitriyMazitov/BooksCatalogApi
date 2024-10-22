using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Services;

namespace Test.Work.BooksCatalogApi.BLL
{
    /// <summary>
	/// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
	/// </summary>
	public static class Entry
    {
        /// <summary>
        /// Добавить службы проекта с логикой
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <returns>Обновленная коллекция служб</returns>
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Entry));
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();

            return services;
        }
    }
}
