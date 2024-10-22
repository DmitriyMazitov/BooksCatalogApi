namespace Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.DeleteBook;

/// <summary>
/// Запрос на удаление книги
/// </summary>
public class DeleteBookRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public DeleteBookRequest(DeleteBookRequest request)
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
    public DeleteBookRequest()
    {
    }
    
    /// <summary>
    /// ID записи
    /// </summary>
    public int? Id { get; set; }
}