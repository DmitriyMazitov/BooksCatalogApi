using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Extensions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthors;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.GetBooks;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.GetBooks;

/// <summary>
/// Обработчик запроса <see cref="GetAuthorsRequest"/>
/// </summary>
public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksResponse>
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public GetBooksQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBooksResponse> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        var query = _dbContext.Books;

        var books = await query
            .Select(cd => new BookItem()
            {
                Id = cd.Id,
                Title = cd.Title,
                Description = cd.Description,
                СhiefAuthor = new AuthorItem
                {
                    Id = cd.СhiefAuthor.Id,
                    FirstName = cd.СhiefAuthor.FirstName,
                    LastName = cd.СhiefAuthor.LastName
                },
                СoAuthors = cd.СoAuthors!.Select(a => new AuthorItem
                {
                    Id = a.Id,
                    FirstName = a.AuthorEntity.FirstName,
                    LastName = a.AuthorEntity.LastName
                }).ToList()
            })
            .OrderBy(request, cd => cd.Id)
            .SkipTake(request)
            .ToListAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new GetBooksResponse(books, count);
    }
}