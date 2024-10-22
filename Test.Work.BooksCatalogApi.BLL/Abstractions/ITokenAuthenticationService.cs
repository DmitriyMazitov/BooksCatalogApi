using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Test.Work.BooksCatalogApi.BLL.Enums;

namespace Test.Work.BooksCatalogApi.BLL.Abstractions
{
    /// <summary>
	/// Сервис работы с токенами.
	/// </summary>
	public interface ITokenAuthenticationService
    {
        /// <summary>
        /// Вид авторизации по токену. (Bearer)
        /// </summary>
        string AuthTokenType { get; }

        /// <summary>
        /// УЦ токенов (мы).
        /// </summary>
        string Authority { get; }

        /// <summary>
        /// Аудитория токенов. Для каждого типа токенов задаем разную аудиторию.
        /// </summary>
        string Audience { get; }

        /// <summary>
        /// Выпускатель токенов (мы).
        /// </summary>
        string ClaimsIssuer { get; }

        /// <summary>
        /// Использовать ли HTTPS.
        /// </summary>
        bool? RequireHttpsMetadata { get; }

        /// <summary>
        /// Создать токен для пользователя с клаймами.
        /// </summary>
        /// <param name="identity">Пользователь с клаймами.</param>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Токен для пользователя с клаймами.</returns>
        string CreateToken(ClaimsIdentity identity, TokenTypes tokenType);

        /// <summary>
        /// Распарсить и проверить токен.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Пользователь с клаймами, которого удалось высунуть из токена.</returns>
        ClaimsPrincipal ValidateToken(string token, TokenTypes tokenType);

        /// <summary>
        /// Получить параметры валидации токенов.
        /// </summary>
        /// <param name="tokenType">Тип токенов.</param>
        /// <returns>Параметры валидации токенов.</returns>
        TokenValidationParameters GetTokenValidationParameters(TokenTypes tokenType);
    }
}
