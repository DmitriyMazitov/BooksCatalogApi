namespace Test.Work.BooksCatalogApi.BLL.Enums
{
    /// <summary>
	/// Тип токена.
	/// </summary>
	public enum TokenTypes
    {
        /// <summary>
        /// Токен авторизации. Работает для авторизации.
        /// </summary>
        Auth = 1,

        /// <summary>
        /// Токен для обновления токена авторизации.
        /// </summary>
        RefreshToken = 2,
    }
}
