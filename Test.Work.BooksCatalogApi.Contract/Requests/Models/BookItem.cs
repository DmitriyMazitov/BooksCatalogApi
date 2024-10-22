namespace Test.Work.BooksCatalogApi.Contracts.Requests.Models;

public class BookItem
{
    /// <summary>
    /// ID книги.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// Название книги.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Описание книги.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Автор книги.
    /// </summary>
    public AuthorItem? СhiefAuthor { get; set; }
    
    /// <summary>
    /// Список соавторов книги
    /// </summary>
    public List<AuthorItem>? СoAuthors { get; set; }
}