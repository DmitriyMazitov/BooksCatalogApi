namespace Test.Work.BooksCatalogApi.BLL.Abstractions
{
    /// <summary>
	/// Сервис проверки прав доступа
	/// </summary>
	public interface IAuthorizationService
    {
        /// <summary>
        /// Отфильтровать пользователей для создания и изменения проектов
        /// </summary>
        void FilterUpdate();
    }
}
