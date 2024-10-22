namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthorById;

/// <summary>
/// Запрос на автора
/// </summary>
public class GetAuthorByIdRequest
{
    private readonly GetAuthorByIdRequest _request;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetAuthorByIdRequest(GetAuthorByIdRequest request)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));

        Id = _request.Id;
    }

    /// <summary>
    /// Id автора
    /// </summary>
    public int Id { get; set; }
}