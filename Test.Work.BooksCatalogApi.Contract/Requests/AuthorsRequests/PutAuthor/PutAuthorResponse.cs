using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PutAuthor;

/// <summary>
/// Ответ на запрос <see cref="PostAuthorRequest"/>
/// </summary>
public class PutAuthorResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="authorItem">Изменённый автор</param>
    public PutAuthorResponse(AuthorItem authorItem) => Author = authorItem;

    /// <summary>
    /// Автор
    /// </summary>
    public AuthorItem Author { get; set; }
}