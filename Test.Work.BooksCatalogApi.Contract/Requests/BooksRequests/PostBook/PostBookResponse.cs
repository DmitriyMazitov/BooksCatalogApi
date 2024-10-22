namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;

/// <summary>
/// Ответ на запрос <see cref="PostAuthorRequest"/>
/// </summary>
public class PostBookResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор новой записи</param>
    public PostBookResponse(int id) => Id = id;

    /// <summary>
    /// Идентификатор новой записи
    /// </summary>
    public int Id { get; set; }
}