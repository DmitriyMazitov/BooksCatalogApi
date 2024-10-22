using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PutBook;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.PutBook;

/// <summary>
/// Команда запроса <see cref="PutBookRequest"/>
/// </summary>
public class PutBookCommand : PutBookRequest, IRequest<PutBookResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PutBookCommand(PutBookRequest request)
        : base(request)
    {
    }
}