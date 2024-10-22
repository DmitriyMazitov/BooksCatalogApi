using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PutBook;

/// <summary>
/// Запрос на создания автора
/// </summary>
public class PutBookRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PutBookRequest(PutBookRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        BookItem = request.BookItem;
    }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public PutBookRequest()
    {
    }

    /// <summary>
    /// Сущность книги
    /// </summary>
    public BookItem? BookItem { get; set; }
}