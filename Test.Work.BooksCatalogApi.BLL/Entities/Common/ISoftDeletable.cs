namespace Test.Work.BooksCatalogApi.BLL.Entities.Common
{
    /// <summary>
	/// Сущность, удаляемая логически
	/// </summary>
	public interface ISoftDeletable
    {
        /// <summary>
        /// Время удаления записи
        /// </summary>
        public DateTime? DeletedAt { get; set; }
    }
}
