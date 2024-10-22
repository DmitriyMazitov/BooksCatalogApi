using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthors;

/// <summary>
/// Ответ на запрос <see cref="GetAuthorsRequest"/>
/// </summary>
public class GetAuthorsResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetAuthorsResponse()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="entities">Список сущностей</param>
    /// <param name="totalCount">Общее количество сущностей</param>
    public GetAuthorsResponse(List<AuthorItem> entities, int totalCount)
    {
        Entities = entities;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Список сущностей
    /// </summary>
    public List<AuthorItem> Entities { get; set; } = default!;

    /// <summary>
    /// Сущность
    /// </summary>
    public AuthorItem Entity { get; set; } = default!;

    /// <summary>
    /// Общее количество сущностей
    /// </summary>
    public int TotalCount { get; set; }
}