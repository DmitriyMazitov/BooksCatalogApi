namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PutAuthor;

/// <summary>
/// Запрос на создания автора
/// </summary>
public class PutAuthorRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PutAuthorRequest(PutAuthorRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        FirstName = request.FirstName;
        LastName = request.LastName;
        Id = request.Id;
    }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public PutAuthorRequest()
    {
    }

    /// <summary>
    /// Имя автора.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// ID записи
    /// </summary>
    public int? Id { get; set; }
}