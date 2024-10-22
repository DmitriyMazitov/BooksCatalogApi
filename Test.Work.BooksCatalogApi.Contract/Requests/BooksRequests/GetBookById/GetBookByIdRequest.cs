namespace Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.GetBookById;

/// <summary>
/// Запрос книги
/// </summary>
public class GetBookByIdRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetBookByIdRequest(GetBookByIdRequest request)
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
    public GetBookByIdRequest()
    {
    }

    /// <summary>
    /// ID записи
    /// </summary>
    public int? Id { get; set; }
}