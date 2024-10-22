using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.DeleteAuthor;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.DeleteBook;

/// <summary>
/// Ответ на запрос <see cref="DeleteAuthorRequest"/>
/// </summary>
public class DeleteBookResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор новой записи</param>
    public DeleteBookResponse(int id) => Id = id;

    /// <summary>
    /// Идентификатор новой записи
    /// </summary>
    public int Id { get; set; }
}