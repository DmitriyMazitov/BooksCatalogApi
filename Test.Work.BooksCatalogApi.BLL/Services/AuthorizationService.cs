using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Exceptions;

namespace Test.Work.BooksCatalogApi.BLL.Services
{
    /// <inheritdoc />
	public class AuthorizationService : IAuthorizationService
    {
        public IUserContext UserContext { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userContext">Контекст пользователя</param>
        public AuthorizationService(IUserContext userContext)
        {
            UserContext = userContext;
        }

        public void FilterUpdate()
        {
            if (UserContext.IsAdministrator.HasValue && UserContext.IsAdministrator.Value)
            {
                return;
            }

            throw new ApplicationExceptionBase("Текущий пользователь не может работать с Book API");
        }
    }
}
