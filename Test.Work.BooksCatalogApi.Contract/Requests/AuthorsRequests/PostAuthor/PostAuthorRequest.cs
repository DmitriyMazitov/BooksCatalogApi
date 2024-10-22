namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;

/// <summary>
/// Запрос на создания автора
/// </summary>
public class PostAuthorRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostAuthorRequest(PostAuthorRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        FirstName = request.FirstName;
        LastName = request.LastName;
    }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public PostAuthorRequest()
    {
    }

    /// <summary>
    /// Имя автора.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    public string LastName { get; set; }
}