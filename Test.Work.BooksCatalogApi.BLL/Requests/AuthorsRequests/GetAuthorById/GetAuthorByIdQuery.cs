using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthorById;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.GetAuthorById;

/// <summary>
/// Команда запроса <see cref="GetAuthorByIdRequest"/>
/// </summary>
public class GetAuthorByIdQuery : IRequest<AuthorItem>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Запрос</param>
    public GetAuthorByIdQuery(int id) => Id = id;

    /// <summary>
    /// ID автора
    /// </summary>
    public int Id { get; set; }
}