using System.ComponentModel.DataAnnotations;
using Test.Work.BooksCatalogApi.BLL.Entities.Common;
using Test.Work.BooksCatalogApi.BLL.Exceptions;

namespace Test.Work.BooksCatalogApi.BLL.Entities;

/// <summary>
/// Сущность "Книга"
/// </summary>
public class Book : EntityBase, ITimeTrackable, IUserTrackable, ISoftDeletable
{
    private string _title;
    private string _description;
    private Author _chiefAuthor;

    /// <summary>
    /// Коструктор
    /// </summary>
    /// <param name="title">Название книги.</param>
    /// <param name="description">Описание книги.</param>
    /// <param name="chiefAuthor">Автор книги</param>
    /// <param name="сoAuthors">Соавторы книги</param>
    public Book(string title, string description, Author chiefAuthor, List<CoAuthor>? сoAuthors = null)
    {
        _title = title;
        _description = description;
        _chiefAuthor = chiefAuthor;
        СoAuthors = сoAuthors;
    }

    /// <summary>
    /// Коструктор по умолчанию
    /// </summary>
    public Book()
    {
    }

    /// <summary>
    /// Название книги.
    /// </summary>
    [MaxLength(50)]
    public string Title {
        get => _title;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new RequiredFieldNotSpecifiedException("Не указано название книги");
            }

            _title = value;
        }
    }

    /// <summary>
    /// Описание книги.
    /// </summary>
    [MaxLength(500)]
    public string Description
    {
        get => _description;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new RequiredFieldNotSpecifiedException("Не указано описание книги");
            }

            _description = value;
        }
    }

    /// <summary>
    /// Автор книги.
    /// </summary>
    public Author СhiefAuthor
    {
        get => _chiefAuthor;
        set => _chiefAuthor = value ?? throw new RequiredFieldNotSpecifiedException("Не указан автор книги");
    }

    /// <summary>
    /// Список соавторов книги
    /// </summary>
    public List<CoAuthor>? СoAuthors { get; set; }

    /// <inheritdoc/>
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc/>
    public DateTime UpdatedAt { get; set; }

    /// <inheritdoc/>
    public Guid CreatedByUserId { get; set; }

    /// <inheritdoc/>
    public Guid ModifiedByUserId { get; set; }

    /// <inheritdoc/>
    public DateTime? DeletedAt { get; set; }
}