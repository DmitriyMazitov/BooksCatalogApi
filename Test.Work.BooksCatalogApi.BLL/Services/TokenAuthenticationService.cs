using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Enums;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.BLL.Models;

namespace Test.Work.BooksCatalogApi.BLL.Services
{
    public class TokenAuthenticationService : ITokenAuthenticationService
    {
        /// <summary>
		/// Конфигурация системы.
		/// </summary>
		private readonly AuthenticationTokenOptions _tokenOptions;

        /// <summary>
        /// Сервис работы с токенами.
        /// </summary>
        /// <param name="tokenOptions">Конфигурация токенов.</param>
        public TokenAuthenticationService(AuthenticationTokenOptions tokenOptions) =>
            _tokenOptions = tokenOptions ?? throw new ArgumentNullException(nameof(tokenOptions));

        /// <summary>
        /// Вид авторизации по токену. (Bearer)
        /// </summary>
        public string AuthTokenType => _tokenOptions.AuthTokenType
            ?? throw new TokenConfigurationException(nameof(_tokenOptions.AuthTokenType));

        /// <summary>
        /// УЦ токенов (мы).
        /// </summary>
        public string Authority => _tokenOptions.Authority
            ?? throw new TokenConfigurationException(nameof(_tokenOptions.Authority));

        /// <summary>
        /// Аудитория токенов. Для каждого типа токенов задаем разную аудиторию.
        /// </summary>
        public string Audience => _tokenOptions.Audience
            ?? throw new TokenConfigurationException(nameof(_tokenOptions.Audience));

        /// <summary>
        /// Выпускатель токенов (мы).
        /// </summary>
        public string ClaimsIssuer => _tokenOptions.Issuer
            ?? throw new TokenConfigurationException(nameof(_tokenOptions.Issuer));

        /// <summary>
        /// Использовать ли HTTPS.
        /// </summary>
        public bool? RequireHttpsMetadata => _tokenOptions.RequireHttpsMetadata;

        /// <summary>
        /// Приватный ключ шифрования.
        /// </summary>
        protected SecurityKey SecurityKey
        {
            get
            {
                var privateKey = _tokenOptions.PrivateKey!;
                var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
                return issuerSigningKey;
            }
        }

        /// <summary>
        /// Ключ шифрования с шифром.
        /// </summary>
        protected SigningCredentials SigningCredentials => new(SecurityKey, SecurityAlgorithms.HmacSha512);

        /// <summary>
        /// Получить параметры валидации токенов.
        /// </summary>
        /// <param name="tokenType">Тип токенов.</param>
        /// <returns>Параметры валидации токенов.</returns>
        public TokenValidationParameters GetTokenValidationParameters(TokenTypes tokenType)
            => tokenType == TokenTypes.Auth
                ? new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = false, // сертификат допускаектся неживой
                    SignatureValidator = (token, _)
                        => new JwtSecurityToken(token), // не валидируем подпись токена
                }
                : new TokenValidationParameters()
                {
                    IssuerSigningKey = SecurityKey,
                    ValidAudience = GetAudienceByTokenType(tokenType),
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(0),
                };

        /// <summary>
        /// Создать токен для пользователя с клаймами.
        /// </summary>
        /// <param name="identity">Пользователь с клаймами.</param>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Токен для пользователя с клаймами.</returns>
        public string CreateToken(ClaimsIdentity identity, TokenTypes tokenType)
        {
            var handler = new JwtSecurityTokenHandler();
            var createdOn = DateTime.UtcNow;
            var expiresOn = createdOn + GetTokenLifetimeByTokenType(tokenType);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenOptions.Issuer,
                Audience = GetAudienceByTokenType(tokenType),
                SigningCredentials = SigningCredentials,
                Subject = identity,
                Expires = expiresOn,
                IssuedAt = createdOn,
                NotBefore = createdOn,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        /// <summary>
        /// Распарсить и проверить токен.
        /// </summary>
        /// <param name="token">Токен.</param>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Пользователь с клаймами, которого удалось высунуть из токена.</returns>
        public ClaimsPrincipal ValidateToken(string token, TokenTypes tokenType)
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = GetTokenValidationParameters(tokenType);
            var principal = handler.ValidateToken(token, validationParameters, out _);
            return principal;
        }

        /// <summary>
        /// Получить время жизни токена по его типу.
        /// </summary>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Время жизни.</returns>
        private TimeSpan GetTokenLifetimeByTokenType(TokenTypes tokenType)
        {
            var ms = tokenType switch
            {
                TokenTypes.Auth => _tokenOptions.AuthTokenExpiresSpan,
                TokenTypes.RefreshToken => _tokenOptions.RefreshTokenExpiresSpan,
                _ => _tokenOptions.DefaultTokenExpiresSpan,
            };
            return TimeSpan.FromMilliseconds(ms);
        }

        /// <summary>
        /// Получить аудиторию токена по его типу.
        /// </summary>
        /// <param name="tokenType">Тип токена.</param>
        /// <returns>Аудитория токена.</returns>
        private string GetAudienceByTokenType(TokenTypes tokenType) => $"{_tokenOptions.Audience}_{tokenType}";
    }
}
