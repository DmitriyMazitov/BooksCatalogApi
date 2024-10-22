using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PostAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.BooksRequests.PostBook;

namespace Test.Work.BooksCatalogApi.BLL.Requests.BooksRequests.PostBook;

/// <summary>
/// Команда запроса <see cref="PostAuthorRequest"/>
/// </summary>
public class PostBookCommand : PostBookRequest, IRequest<PostBookResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostBookCommand(PostBookRequest request)
        : base(request)
    {
    }
}