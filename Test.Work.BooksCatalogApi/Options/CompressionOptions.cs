namespace Test.Work.BooksCatalogApi.Options
{
    /// <summary>
	/// Опции сжатия динамического ответа
	/// </summary>
	public class CompressionOptions
    {
        public CompressionOptions()
            => MimeTypes = new List<string>();

        /// <summary>
        /// Типы контента для сжатия
        /// </summary>
        public List<string> MimeTypes { get; }
    }
}
