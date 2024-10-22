namespace Test.Work.BooksCatalogApi.Cors
{
    /// <summary>
	/// Точка входа сервисов CORS для приложения
	/// </summary>
	public static class Entry
    {
        /// <summary>
        /// Добавить CORS в службы приложения
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        /// <param name="services">Службы приложения</param>
        /// <returns>Службы приложения с CORS</returns>
        public static IServiceCollection AddCustomCors(this IServiceCollection services) =>
            services.AddCors(
                options =>
                    // Использовать: application.UseCors("PolicyName")
                    // или [EnableCors("PolicyName")]
                    options.AddPolicy(
                        CorsPolicies.AllowAny,
                        x => x
                            .WithOrigins("http://localhost:5173")
                .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders(new[] { "Content-Disposition" })));
    }
}
