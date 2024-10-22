using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthorById;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.GetBookById;

/// <summary>
/// Команда запроса <see cref="GetAuthorByIdRequest"/>
/// </summary>
public class GetBookByIdQuery : IRequest<BookItem>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Запрос</param>
    public GetBookByIdQuery(int id) => Id = id;

    /// <summary>
    /// ID автора
    /// </summary>
    public int Id { get; set; }
}