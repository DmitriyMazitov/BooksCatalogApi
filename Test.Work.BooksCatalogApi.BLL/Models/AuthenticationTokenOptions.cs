namespace Test.Work.BooksCatalogApi.BLL.Models
{
    /// <summary>
	/// Конфигурация токенов.
	/// </summary>
	public class AuthenticationTokenOptions
    {
        /// <summary>
        /// Приватный ключ шифрования.
        /// </summary>
        public string? PrivateKey { get; set; }

        /// <summary>
        /// УЦ токенов (мы).
        /// </summary>
        public string? Authority { get; set; }

        /// <summary>
        /// Использовать ли HTTPS.
        /// </summary>
        public bool RequireHttpsMetadata { get; set; }

        /// <summary>
        /// Аудитория токенов. Для каждого типа токенов задаем разную аудиторию.
        /// </summary>
        public string? Audience { get; set; }

        /// <summary>
        /// Выпускатель токенов (мы).
        /// </summary>
        public string? Issuer { get; set; }

        /// <summary>
        /// Вид авторизации по токену. (Bearer)
        /// </summary>
        public string? AuthTokenType { get; set; }

        /// <summary>
        /// Время жизни токена аутентификации.
        /// </summary>
        public long AuthTokenExpiresSpan { get; set; }

        /// <summary>
        /// Время жизни токена для обновления токена аутентификации.
        /// </summary>
        public long RefreshTokenExpiresSpan { get; set; }

        /// <summary>
        /// Время жизни токена для подтверждения мыла.
        /// </summary>
        public long EmailConfirmationTokenExpiresSpan { get; set; }

        /// <summary>
        /// Время жизни токена интеграции.
        /// </summary>
        public long DefaultTokenExpiresSpan { get; set; }
    }
}
