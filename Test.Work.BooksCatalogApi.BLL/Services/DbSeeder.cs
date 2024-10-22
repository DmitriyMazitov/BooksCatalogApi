using Test.Work.BooksCatalogApi.BLL.Abstractions;

namespace Test.Work.BooksCatalogApi.BLL.Services
{
    /// <inheritdoc/>
	public class DbSeeder : IDbSeeder
    {
        /// <summary>
		/// Конструктор
		/// </summary>
		public DbSeeder()
        {
        }

        /// <inheritdoc/>
        public async Task SeedAsync(IDbContext dbContext)
        {
            //await dbContext.SaveChangesAsync();
        }
    }
}
