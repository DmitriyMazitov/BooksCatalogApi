namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.DeleteAuthor;

/// <summary>
/// Ответ на запрос <see cref="DeleteAuthorRequest"/>
/// </summary>
public class DeleteAuthorResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор новой записи</param>
    public DeleteAuthorResponse(int id) => Id = id;

    /// <summary>
    /// Идентификатор новой записи
    /// </summary>
    public int Id { get; set; }
}