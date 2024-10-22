using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Extensions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthors;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.GetAuthors;

/// <summary>
/// Обработчик запроса <see cref="GetAuthorsRequest"/>
/// </summary>
public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, GetAuthorsResponse>
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public GetAuthorsQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAuthorsResponse> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var query = _dbContext.Authors;

        var authors = await query
            .Select(cd => new AuthorItem
            {
                Id = cd.Id,
                FirstName = cd.FirstName,
                LastName = cd.LastName
            })
            .OrderBy(request, cd => cd.Id)
            .SkipTake(request)
            .ToListAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new GetAuthorsResponse(authors, count);
    }
}