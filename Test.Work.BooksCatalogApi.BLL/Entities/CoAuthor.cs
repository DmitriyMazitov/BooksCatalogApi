using Test.Work.BooksCatalogApi.BLL.Entities.Common;
using Test.Work.BooksCatalogApi.BLL.Exceptions;

namespace Test.Work.BooksCatalogApi.BLL.Entities;

public class CoAuthor : EntityBase, ITimeTrackable, IUserTrackable, ISoftDeletable
{
    private Author _authorEntity;
    private Book _bookEntity;

    /// <summary>
    /// Коструктор
    /// </summary>
    /// <param name="authorEntity">Сущность автора</param>
    /// <param name="bookEntity">Сущность книги</param>
    public CoAuthor(Author authorEntity, Book bookEntity)
    {
        _authorEntity = authorEntity;
        _bookEntity = bookEntity;
    }

    /// <summary>
    /// Коструктор по умолчанию
    /// </summary>
    public CoAuthor()
    {
    }

    /// <summary>
    /// id автора
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// Сущность "Автор"
    /// </summary>
    public Author AuthorEntity
    {
        get => _authorEntity;
        set
        {
            _authorEntity = value is null ? throw new RequiredFieldNotSpecifiedException("Автор") : value;
            AuthorId = value.Id;
        }
    }

    /// <summary>
    /// id книги
    /// </summary>
    public int BookId { get; set; }

    /// <summary>
    /// Сущность "Книга"
    /// </summary>
    public Book BookEntity
    {
        get => _bookEntity;
        set
        {
            _bookEntity = value is null ? throw new RequiredFieldNotSpecifiedException("Книга") : value;
            BookId = value.Id;
        }
    }

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