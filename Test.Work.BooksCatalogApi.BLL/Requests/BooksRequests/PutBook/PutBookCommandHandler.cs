using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PutBook;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.PutBook;

/// <summary>
/// Обработчик запроса <see cref="PutBookRequest"/>
/// </summary>
public class PutBookCommandHandler : IRequestHandler<PutBookCommand, PutBookResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public PutBookCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<PutBookResponse> Handle(PutBookCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.BookItem?.Id == null)
        {
            throw new RequiredFieldNotSpecifiedException(request.BookItem?.Id.ToString()!);
        }

        var currentBook = await _dbContext.Books.Where(a => a.Id == request.BookItem.Id).FirstOrDefaultAsync(cancellationToken);

        if (currentBook == null)
        {
            throw new EntityNotFoundException<Book>((int)request.BookItem.Id);
        }

        if (string.IsNullOrEmpty(request.BookItem.Title) && string.IsNullOrEmpty(request.BookItem.Description) && request.BookItem.СhiefAuthor != null)
        {
            throw new RequiredFieldNotSpecifiedException();
        }
        
        if (request.BookItem.Title != null)
        {
            currentBook.Title = request.BookItem.Title;
        }

        if (request.BookItem.Description != null)
        {
            currentBook.Description = request.BookItem.Description;
        }

        if (request.BookItem.СhiefAuthor != null)
        {
            currentBook.СhiefAuthor = new Author
            {
                Id = request.BookItem.СhiefAuthor.Id,
                FirstName = request.BookItem.СhiefAuthor.FirstName!,
                LastName = request.BookItem.СhiefAuthor.LastName!
            };
        }

        if (request.BookItem.СoAuthors != null)
        {
            currentBook.СoAuthors = request.BookItem.СoAuthors.Select(a => new CoAuthor
            {
                Id = a.Id,
                BookId = (int)request.BookItem.Id,
                BookEntity = new Book
                {
                    Id = (int)request.BookItem.Id,
                    Title = request.BookItem.Title!,
                    Description = request.BookItem.Description!
                },
                AuthorId = a.Id,
                AuthorEntity = new Author
                {
                    Id = a.Id,
                    FirstName = a.FirstName!,
                    LastName = a.LastName!
                }
            }).ToList();
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PutBookResponse(request.BookItem);
    }
}