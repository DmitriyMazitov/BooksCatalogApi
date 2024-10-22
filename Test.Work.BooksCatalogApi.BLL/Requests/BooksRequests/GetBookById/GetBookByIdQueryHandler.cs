using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthorById;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.GetBookById;

/// <summary>
/// Обработчик запроса <see cref="GetAuthorByIdRequest"/>
/// </summary>
public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookItem>
{
    private readonly IDbContext _dbContext;


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public GetBookByIdQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BookItem> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var currentBook = await _dbContext.Books
            .Select(cd => new BookItem
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
            .Where(a => a.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (currentBook == null)
        {
            throw new EntityNotFoundException<Book>(request.Id);
        }

        return currentBook;
    }
}