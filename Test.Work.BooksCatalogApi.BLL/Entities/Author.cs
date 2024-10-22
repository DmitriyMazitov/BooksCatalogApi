using System.ComponentModel.DataAnnotations;
using Test.Work.BooksCatalogApi.BLL.Entities.Common;
using Test.Work.BooksCatalogApi.BLL.Exceptions;

namespace Test.Work.BooksCatalogApi.BLL.Entities;

/// <summary>
/// Сущность "Автор"
/// </summary>
public class Author : EntityBase, ITimeTrackable, IUserTrackable, ISoftDeletable
{
    private string _lastName;
    private string _firstName;

    /// <summary>
    /// Коструктор
    /// </summary>
    /// <param name="lastName">Имя автора</param>
    /// <param name="firstName">Фамилия автора</param>
    public Author(string lastName, string firstName)
    {
        _lastName = lastName;
        _firstName = firstName;
    }

    /// <summary>
    /// Коструктор по умолчанию
    /// </summary>
    public Author()
    {
    }

    /// <summary>
    /// Имя автора.
    /// </summary>
    [MinLength(2)]
    [MaxLength(25)]
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new RequiredFieldNotSpecifiedException("Имя автора");
            }

            _firstName = value;
        }
    }

    /// <summary>
    /// Фамилия автора.
    /// </summary>
    [MinLength(2)]
    [MaxLength(25)]
    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new RequiredFieldNotSpecifiedException("Фамилия автора");
            }

            _lastName = value;
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