using System.Security.Claims;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Enums;

namespace Test.Work.BooksCatalogApi.Authentication
{
    /// <inheritdoc/>
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="httpContextAccessor">Адаптер Http-context'а</param>
        public UserContext(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        /// <inheritdoc/>
        public Guid CurrentUserId => Guid.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)
            ? userId
            : Guid.Empty;

        /// <inheritdoc/>
        public bool? IsAdministrator => bool.TryParse(User?.FindFirst(ClaimNames.IsAdministrator)?.Value, out var isAdministrator)
            ? isAdministrator
            : default;
        
        /// <inheritdoc/>
        public bool? IsSuperAdministrator => bool.TryParse(User?.FindFirst(ClaimNames.IsSuperAdministrator)?.Value, out var isSuperAdministrator)
            ? isSuperAdministrator
            : default;

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    }
}