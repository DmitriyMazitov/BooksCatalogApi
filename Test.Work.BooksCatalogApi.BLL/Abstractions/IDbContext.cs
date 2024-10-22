using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Entities;

namespace Test.Work.BooksCatalogApi.BLL.Abstractions
{
    /// <summary>
	/// Контекст EF Core для приложения
	/// </summary>
	public interface IDbContext
    {
        /// <summary>
        /// Книги
        /// </summary>
        DbSet<Book> Books { get; }

        /// <summary>
        /// Авторы
        /// </summary>
        public DbSet<Author> Authors { get; }

        /// <summary>
        /// Соавторы
        /// </summary>
        public DbSet<CoAuthor> CoAuthors { get; }

        /// <summary>
        /// БД в памяти
        /// </summary>
        bool IsInMemory { get; }

        /// <summary>
        /// Очистить UnitOfWork
        /// </summary>
        void Clean();

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Применить изменения при успешном сохранении в контекст</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Количество обновленных записей</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Количество обновленных записей</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
