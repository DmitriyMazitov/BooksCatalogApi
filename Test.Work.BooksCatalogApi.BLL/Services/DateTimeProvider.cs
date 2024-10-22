using Test.Work.BooksCatalogApi.BLL.Abstractions;

namespace Test.Work.BooksCatalogApi.BLL.Services
{
    /// <inheritdoc/>
	public class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
