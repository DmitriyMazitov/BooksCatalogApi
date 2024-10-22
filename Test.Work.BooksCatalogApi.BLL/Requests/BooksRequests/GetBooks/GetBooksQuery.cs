using MediatR;
using Test.Work.BooksCatalogApi.BLL.Models;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthors;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.GetBooks;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.GetBooks;

/// <summary>
/// Команда запроса <see cref="GetAuthorsRequest"/>
/// </summary>
public class GetBooksQuery : GetBooksRequest, IRequest<GetBooksResponse>, IOrderByQuery, IPaginationQuery
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetBooksQuery(GetBooksRequest request)
        : base(request)
    {
    }
}