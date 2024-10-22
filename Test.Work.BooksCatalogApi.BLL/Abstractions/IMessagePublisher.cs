namespace Test.Work.BooksCatalogApi.BLL.Abstractions
{
    /// <summary>
	/// Публикатор сообщений в очередь
	/// </summary>
	public interface IMessagePublisher
    {
        /// <summary>
        /// Опубликовать сообщение в очередь
        /// </summary>
        /// <typeparam name="TMessage">Тип сообщения</typeparam>
        /// <param name="messages">Сообщения</param>
        /// <returns>-</returns>
        Task PublishAsync<TMessage>(params TMessage[] messages)
            where TMessage : class;
    }
}
