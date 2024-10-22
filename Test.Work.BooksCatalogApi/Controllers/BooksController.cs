using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.DeleteBook;
using Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.GetBookById;
using Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.GetBooks;
using Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.PostBook;
using Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.PutBook;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.GetBooks;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PostBook;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PutBook;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;
using Test.Work.BooksCatalogApi.Contracts.Services;
using Test.Work.BooksCatalogApi.Versioning;

namespace Test.Work.BooksCatalogApi.Controllers;


/// <summary>
/// Контроллер книг
/// </summary>
[ApiVersion(ApiVersions.V1)]
[Authorize]
public class BooksController : ApiControllerBase
{
    /// <summary>
    /// Создание книги
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной записи</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostBookResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<PostBookResponse> CreateBookAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostBookRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostBookCommand(request), cancellationToken);

    /// <summary>
    /// Получение списка книг
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список найденных книг</returns>
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetBooksResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<GetBooksResponse> GetBooksAsync(
        [FromServices] IMediator mediator,
        [FromQuery] GetBooksRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetBooksQuery(request), cancellationToken);

    /// <summary>
    /// Удаление книги по Id
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Unit))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<Unit> DeleteBookAsync(
        [FromServices] IMediator mediator,
        [FromRoute] int id,
        CancellationToken cancellationToken)
        => await mediator.Send(new DeleteBookCommand(id), cancellationToken);

    /// <summary>
    /// Получение книги по id
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="id">ИД книги</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>сущность книги</returns>
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(BookItem))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<BookItem> GetAuthorByIdAsync(
        [FromServices] IMediator mediator,
        [FromRoute] int id,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetBookByIdQuery(id), cancellationToken);

    /// <summary>
    /// Редактирование книги
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность изменённой записи</returns>
    [HttpPut]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PutBookResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<PutBookResponse> EditBookAsync(
        [FromServices] IMediator mediator,
        [FromBody] PutBookRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PutBookCommand(request), cancellationToken);
}