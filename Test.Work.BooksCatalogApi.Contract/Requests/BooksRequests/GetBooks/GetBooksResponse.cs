using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.GetBooks;

/// <summary>
/// Ответ на запрос <see cref="GetBooksRequest"/>
/// </summary>
public class GetBooksResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetBooksResponse()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="entities">Список сущностей</param>
    /// <param name="totalCount">Общее количество сущностей</param>
    public GetBooksResponse(List<BookItem> entities, int totalCount)
    {
        Entities = entities;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Список сущностей
    /// </summary>
    public List<BookItem> Entities { get; set; } = default!;

    /// <summary>
    /// Сущность
    /// </summary>
    public BookItem Entity { get; set; } = default!;

    /// <summary>
    /// Общее количество сущностей
    /// </summary>
    public int TotalCount { get; set; }
}