namespace Test.Work.BooksCatalogApi.BLL.Abstractions
{
    /// <summary>
	/// Контекст текущего пользователя
	/// </summary>
	public interface IUserContext
    {
        /// <summary>
        /// ИД текущего пользователя
        /// </summary>
        Guid CurrentUserId { get; }

        /// <summary>
        /// Администратор
        /// </summary>
        bool? IsAdministrator { get; }
        
        /// <summary>
        /// Супер администратор
        /// </summary>
        bool? IsSuperAdministrator { get; }
    }
}
