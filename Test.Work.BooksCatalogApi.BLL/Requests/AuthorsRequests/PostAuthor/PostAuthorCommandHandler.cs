using MediatR;
using Test.Work.BooksCatalogApi.BLL.Abstractions;
using Test.Work.BooksCatalogApi.BLL.Common;
using Test.Work.BooksCatalogApi.BLL.Entities;
using Test.Work.BooksCatalogApi.BLL.Enums;
using Test.Work.BooksCatalogApi.BLL.Exceptions;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.PostAuthor;

/// <summary>
/// Обработчик запроса <see cref="PostAuthorRequest"/>
/// </summary>
public class PostAuthorCommandHandler : IRequestHandler<PostAuthorCommand, PostAuthorResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IAuthorizationService _authorizationService;


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="authorizationService">Контекст авторизации</param>
    public PostAuthorCommandHandler(IDbContext dbContext, IAuthorizationService authorizationService)
    {
        _dbContext = dbContext;
        _authorizationService = authorizationService;
    }
    public async Task<PostAuthorResponse> Handle(PostAuthorCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        _authorizationService.FilterUpdate();

        if (string.IsNullOrEmpty(request.FirstName))
        {
            throw new RequiredFieldNotSpecifiedException(request.FirstName);
        }

        if (string.IsNullOrEmpty(request.LastName))
        {
            throw new RequiredFieldNotSpecifiedException(request.LastName);
        }

        if (Helpers.GetLanguageType(request.FirstName) != Helpers.GetLanguageType(request.LastName) 
            || Helpers.GetLanguageType(request.FirstName) == LanguageType.UnspecifiedLanguage || Helpers.GetLanguageType(request.LastName) == LanguageType.UnspecifiedLanguage)
        {
            throw new RequiredFieldsFilledIncorrectlyException(request.FirstName, request.LastName);
        }

        var author = new Author(firstName: request.FirstName, lastName: request.LastName);

        await _dbContext.Authors.AddAsync(author, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostAuthorResponse(author.Id);
    }
}