namespace Test.Work.BooksCatalogApi.BLL.Exceptions;

/// <summary>
/// Исключение "обязательные поля заполнены не верно"
/// </summary>
public class RequiredFieldsFilledIncorrectlyException : ApplicationExceptionBase
{
    /// <summary>
    /// Исключение "обязательные поля заполнены не верно"
    /// </summary>
    public RequiredFieldsFilledIncorrectlyException()
        : base("Обязательное поле заполнено не верно")
    {
    }

    /// <summary>
    /// Исключение "обязательные поля заполнены не верно"
    /// </summary>
    /// <param name="firstName">Обязательное для заполнения поле</param>
    /// <param name="lastName">Обязательное для заполнения поле</param>
    public RequiredFieldsFilledIncorrectlyException(string firstName, string lastName)
        : base($"Поля 'FirstName' и 'LastName' заполнены на разных языках '{firstName}' и '{lastName}'")
    {
    }
}