using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.DeleteAuthor;
using Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.GetAuthorById;
using Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.GetAuthors;
using Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.PostAuthor;
using Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.PutAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthors;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PutAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;
using Test.Work.BooksCatalogApi.Contracts.Services;
using Test.Work.BooksCatalogApi.Versioning;

namespace Test.Work.BooksCatalogApi.Controllers;

/// <summary>
/// Контроллер авторов
/// </summary>
[ApiVersion(ApiVersions.V1)]
[Authorize]
public class AuthorsController : ApiControllerBase
{
    /// <summary>
    /// Создание автора
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданной записи</returns>
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PostAuthorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<PostAuthorResponse> CreateAuthorAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostAuthorRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostAuthorCommand(request), cancellationToken);

    /// <summary>
    /// Редактирование автора
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Сущность изменённой записи</returns>
    [HttpPut]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PutAuthorResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<PutAuthorResponse> EditAuthorAsync(
        [FromServices] IMediator mediator,
        [FromBody] PutAuthorRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PutAuthorCommand(request), cancellationToken);

    /// <summary>
    /// Получение списка авторов
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список найденных авторов</returns>
    [HttpGet]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetAuthorsResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<GetAuthorsResponse> GetAuthorsAsync(
        [FromServices] IMediator mediator,
        [FromQuery] GetAuthorsRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetAuthorsQuery(request), cancellationToken);

    /// <summary>
    /// Получение автора по id
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="id">ИД автора</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>сущность автора</returns>
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AuthorItem))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<AuthorItem> GetAuthorByIdAsync(
        [FromServices] IMediator mediator,
        [FromRoute] int id,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);

    /// <summary>
    /// Удаление автора по Id
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(Unit))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetailsResponse))]
    public async Task<Unit> DeleteAuthorAsync(
        [FromServices] IMediator mediator,
        [FromRoute] int id,
        CancellationToken cancellationToken)
        => await mediator.Send(new DeleteAuthorCommand(id), cancellationToken);
}