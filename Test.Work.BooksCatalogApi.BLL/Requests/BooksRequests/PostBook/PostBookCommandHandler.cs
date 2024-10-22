using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.PostBook;

/// <summary>
/// Обработчик запроса <see cref="PostAuthorRequest"/>
/// </summary>
public class PostBookCommandHandler : IRequestHandler<PostBookCommand, PostBookResponse>
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public PostBookCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<PostBookResponse> Handle(PostBookCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        if (string.IsNullOrEmpty(request.Title))
        {
            throw new RequiredFieldNotSpecifiedException(request.Title);
        }

        if (string.IsNullOrEmpty(request.Description))
        {
            throw new RequiredFieldNotSpecifiedException(request.Description);
        }

        var chiefAuthor = await _dbContext.Authors.Where(x => x.Id == request.СhiefAuthor.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (chiefAuthor == null)
        {
            throw new EntityNotFoundException<Author>(request.СhiefAuthor.Id);
        }

        List<int>? coAuthorsIds = null;
        
        Book newBook = new Book(request.Title, request.Description, chiefAuthor);

        if (request.СoAuthors != null)
        {
            coAuthorsIds = request.СoAuthors.Select(a => a.Id).ToList();
        }

        if (coAuthorsIds != null)
        {
            if (coAuthorsIds.Any(a => a == request.СhiefAuthor.Id))
            {
                throw new DuplicateEntitiesException(request.Title, request.СhiefAuthor.Id);
            }

            var coAuthors = await _dbContext.Authors.Where(x => coAuthorsIds.Contains(x.Id)).ToListAsync(cancellationToken);

            newBook.СoAuthors = coAuthors.Select(a => new CoAuthor
            {
                AuthorId = a.Id,
            }).ToList();
        }
        
        await _dbContext.Books.AddAsync(newBook, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostBookResponse(newBook.Id);
    }
}