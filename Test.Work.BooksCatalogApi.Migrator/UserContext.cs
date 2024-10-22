using Test.Work.BooksCatalogApi.BLL.Abstractions;

namespace Test.Work.BooksCatalogApi.Migrator
{
    /// <summary>
	/// Мок юзерконтекста для контекста БД
	/// </summary>
	internal class UserContext : IUserContext
    {
        /// <inheritdoc/>
        public Guid CurrentUserId => throw new NotImplementedException();

        public bool? IsAdministrator => throw new NotImplementedException();
        
        public bool? IsSuperAdministrator => throw new NotImplementedException();
    }
}
