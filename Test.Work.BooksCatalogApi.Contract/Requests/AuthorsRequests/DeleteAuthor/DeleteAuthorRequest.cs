namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.DeleteAuthor;

/// <summary>
/// Запрос на удаление автора
/// </summary>
public class DeleteAuthorRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public DeleteAuthorRequest(DeleteAuthorRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        Id = request.Id;
    }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public DeleteAuthorRequest()
    {
    }
    
    /// <summary>
    /// ID записи
    /// </summary>
    public int? Id { get; set; }
}