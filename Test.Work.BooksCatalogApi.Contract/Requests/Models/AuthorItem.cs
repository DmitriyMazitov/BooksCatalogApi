namespace Test.Work.BooksCatalogApi.Contracts.Requests.Models;

public class AuthorItem
{
    /// <summary>
    /// ID записи
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя автора.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    public string? LastName { get; set; }
}