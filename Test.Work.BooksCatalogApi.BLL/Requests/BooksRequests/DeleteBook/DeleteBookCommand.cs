using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.DeleteBook;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.DeleteBook;

/// <summary>
/// Команда запроса <see cref="DeleteBookRequest"/>
/// </summary>
public class DeleteBookCommand : DeleteBookRequest, IRequest<Unit>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="bookId">Id книги</param>
    public DeleteBookCommand(int bookId)
    {
        BookId = bookId;
    }

    public int BookId { get; set; }
}