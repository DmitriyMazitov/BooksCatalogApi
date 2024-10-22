using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PutAuthor;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.PutAuthor;

/// <summary>
/// Команда запроса <see cref="PutAuthorRequest"/>
/// </summary>
public class PutAuthorCommand : PutAuthorRequest, IRequest<PutAuthorResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PutAuthorCommand(PutAuthorRequest request)
        : base(request)
    {
    }
}