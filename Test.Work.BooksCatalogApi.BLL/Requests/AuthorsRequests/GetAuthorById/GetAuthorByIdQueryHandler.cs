using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthorById;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.GetAuthorById;

/// <summary>
/// Обработчик запроса <see cref="GetAuthorByIdRequest"/>
/// </summary>
public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorItem>
{
    private readonly IDbContext _dbContext;
    private readonly IAuthorizationService _authorizationService;


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="authorizationService">Контекст авторизации</param>
    public GetAuthorByIdQueryHandler(IDbContext dbContext, IAuthorizationService authorizationService)
    {
        _dbContext = dbContext;
        _authorizationService = authorizationService;
    }

    public async Task<AuthorItem> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        _authorizationService.FilterUpdate();

        var currentAuthor = await _dbContext.Authors
            .Select(cd => new AuthorItem
                {
                    Id = cd.Id,
                    FirstName = cd.FirstName,
                    LastName = cd.LastName
                })
            .Where(a => a.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (currentAuthor == null)
        {
            throw new EntityNotFoundException<Author>(request.Id);
        }

        return currentAuthor;
    }
}