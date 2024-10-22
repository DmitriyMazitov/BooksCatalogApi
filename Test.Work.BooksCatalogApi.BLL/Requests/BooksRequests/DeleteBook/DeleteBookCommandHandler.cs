using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.DeleteAuthor;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.DeleteBook;

/// <summary>
/// Обработчик запроса <see cref="DeleteAuthorRequest"/>
/// </summary>
public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public DeleteBookCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var currentBook = await _dbContext.Books.Where(a => a.Id == request.BookId).FirstOrDefaultAsync(cancellationToken);

        if (currentBook == null)
        {
            throw new EntityNotFoundException<Book>(request.BookId);
        }
        
        _dbContext.Books.Remove(currentBook);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}