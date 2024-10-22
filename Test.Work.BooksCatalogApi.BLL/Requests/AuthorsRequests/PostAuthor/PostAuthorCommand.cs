using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.PostAuthor;

/// <summary>
/// Команда запроса <see cref="PostAuthorRequest"/>
/// </summary>
public class PostAuthorCommand : PostAuthorRequest, IRequest<PostAuthorResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostAuthorCommand(PostAuthorRequest request)
        : base(request)
    {
    }
}