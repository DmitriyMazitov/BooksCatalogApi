using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.DeleteAuthor;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.DeleteAuthor;

/// <summary>
/// Обработчик запроса <see cref="DeleteAuthorRequest"/>
/// </summary>
public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Unit>
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public DeleteAuthorCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        var currentAuthor = await _dbContext.Authors.Where(a => a.Id == request.AuthorId).FirstOrDefaultAsync(cancellationToken);

        if (currentAuthor == null)
        {
            throw new EntityNotFoundException<Author>(request.AuthorId);
        }
        
        _dbContext.Authors.Remove(currentAuthor);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}