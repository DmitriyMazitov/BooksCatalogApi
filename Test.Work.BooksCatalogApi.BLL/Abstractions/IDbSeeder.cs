namespace Test.Work.BooksCatalogApi.BLL.Abstractions
{
    /// <summary>
	/// Сервис добавления записей в БД
	/// </summary>
	public interface IDbSeeder
    {
        /// <summary>
        /// Добавить записи
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <returns>-</returns>
        public Task SeedAsync(IDbContext dbContext);
    }
}
