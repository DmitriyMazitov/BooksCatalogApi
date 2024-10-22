using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PostBook;

/// <summary>
/// Запрос на создания автора
/// </summary>
public class PostBookRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostBookRequest(PostBookRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        Title = request.Title;
        Description = request.Description;
        СhiefAuthor = request.СhiefAuthor;
        СoAuthors = request.СoAuthors;
    }

    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public PostBookRequest()
    {
    }

    /// <summary>
    /// Название книги.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Описание книги.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Автор книги.
    /// </summary>
    public AuthorItem СhiefAuthor { get; set; }

    /// <summary>
    /// Список соавторов книги
    /// </summary>
    public List<AuthorItem>? СoAuthors { get; set; }
}
