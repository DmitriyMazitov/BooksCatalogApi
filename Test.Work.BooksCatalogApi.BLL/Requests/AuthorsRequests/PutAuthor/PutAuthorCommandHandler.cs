using MediatR;
using Microsoft.EntityFrameworkCore;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Common;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Enums;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PutAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.Models;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.PutAuthor;

/// <summary>
/// Обработчик запроса <see cref="PutAuthorRequest"/>
/// </summary>
public class PutAuthorCommandHandler : IRequestHandler<PutAuthorCommand, PutAuthorResponse>
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public PutAuthorCommandHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<PutAuthorResponse> Handle(PutAuthorCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (request.Id == null)
        {
            throw new RequiredFieldNotSpecifiedException(request.Id.ToString()!);
        }

        var currentAuthor = await _dbContext.Authors.Where(a => a.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (currentAuthor == null)
        {
            throw new EntityNotFoundException<Author>((int)request.Id);
        }

        if (string.IsNullOrEmpty(request.FirstName) && string.IsNullOrEmpty(request.LastName))
        {
            throw new RequiredFieldNotSpecifiedException();
        }

        if (!string.IsNullOrEmpty(request.FirstName) && Helpers.GetLanguageType(request.FirstName) != Helpers.GetLanguageType(currentAuthor.LastName) && Helpers.GetLanguageType(request.FirstName) == LanguageType.UnspecifiedLanguage)
        {
            throw new RequiredFieldsFilledIncorrectlyException(request.FirstName, currentAuthor.LastName);
        }
        
        if (!string.IsNullOrEmpty(request.LastName) && Helpers.GetLanguageType(request.LastName) != Helpers.GetLanguageType(currentAuthor.FirstName) && Helpers.GetLanguageType(request.LastName) == LanguageType.UnspecifiedLanguage)
        {
            throw new RequiredFieldsFilledIncorrectlyException(request.LastName, currentAuthor.FirstName);
        }

        if (request.FirstName != null)
        {
            currentAuthor.FirstName = request.FirstName;
        }

        if (request.LastName != null)
        {
            currentAuthor.LastName = request.LastName;
        }

        var author = new AuthorItem
        {
            FirstName = currentAuthor.FirstName,
            LastName = currentAuthor.LastName,
            Id = currentAuthor.Id
        };

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PutAuthorResponse(author);
    }
}