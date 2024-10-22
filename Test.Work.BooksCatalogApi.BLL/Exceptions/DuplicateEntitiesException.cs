namespace Test.Work.BooksCatalogApi.BLL.Exceptions;

/// <summary>
/// Исключение "дублирование сущностей"
/// </summary>
public class DuplicateEntitiesException : ApplicationExceptionBase
{
    /// <summary>
    /// Исключение "дублирование сущностей"
    /// </summary>
    public DuplicateEntitiesException()
        : base("Дублирование сущностей")
    {
    }

    /// <summary>
    /// Исключение "дублирование сущностей"
    /// </summary>
    /// <param name="bookTitle">Наименование книги</param>
    /// <param name="authorId">ИД автора</param>
    public DuplicateEntitiesException(string bookTitle, int authorId)
        : base($"Книга '{bookTitle}' не может содержать в качестве автора и соавтора одного и того же автора  '{authorId}'.")
    {
    }
}