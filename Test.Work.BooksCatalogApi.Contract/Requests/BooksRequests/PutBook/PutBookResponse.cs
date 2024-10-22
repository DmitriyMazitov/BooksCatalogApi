using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PutBook;

/// <summary>
/// Ответ на запрос <see cref="PostAuthorRequest"/>
/// </summary>
public class PutBookResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="bookItem">Изменённый автор</param>
    public PutBookResponse(BookItem bookItem) => Book = bookItem;

    /// <summary>
    /// Книга
    /// </summary>
    public BookItem Book { get; set; }
}