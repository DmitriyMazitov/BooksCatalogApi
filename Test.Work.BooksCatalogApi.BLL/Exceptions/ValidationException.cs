namespace Test.Work.BooksCatalogApi.BLL.Exceptions
{
    /// <summary>
	/// Исключение валидации данных в домене
	/// </summary>
	public class ValidationException : ApplicationExceptionBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ValidationException()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public ValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Конструктор с коллекцией ошибок
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="exceptionsByField">Коллекций ошибок</param>
        public ValidationException(
            string message,
            Dictionary<string, string> exceptionsByField)
            : base(message)
                => ExceptionsByField = exceptionsByField;

        /// <summary>
        /// Конструктор со словарем ошибок
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="exceptions">Коллекция ошибок</param>
        public ValidationException(
            string message,
            IEnumerable<string>? exceptions)
            : base(message)
                => Exceptions = exceptions;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутренняя ошибка</param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Ошибки валиадции
        /// </summary>
        public IEnumerable<string>? Exceptions { get; set; }

        /// <summary>
        /// Ошибки по полю сущности
        /// </summary>
        public Dictionary<string, string>? ExceptionsByField { get; set; }
    }
}
