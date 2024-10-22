using MediatR;
using Test.Work.BooksCatalogApi.BLL.Models;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthors;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.GetAuthors;

/// <summary>
/// Команда запроса <see cref="GetAuthorsRequest"/>
/// </summary>
public class GetAuthorsQuery : GetAuthorsRequest, IRequest<GetAuthorsResponse>, IOrderByQuery, IPaginationQuery
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetAuthorsQuery(GetAuthorsRequest request)
        : base(request)
    {
    }
}