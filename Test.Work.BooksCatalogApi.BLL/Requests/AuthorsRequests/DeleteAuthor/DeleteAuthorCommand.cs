using MediatR;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.DeleteAuthor;
using Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.PutAuthor;

namespace Test.Work.BooksCatalogApi.BLL.Requests.AuthorsRequests.DeleteAuthor;

/// <summary>
/// Команда запроса <see cref="PutAuthorRequest"/>
/// </summary>
public class DeleteAuthorCommand : DeleteAuthorRequest, IRequest<Unit>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="authorId">Id автора</param>
    public DeleteAuthorCommand(int authorId)
    {
        AuthorId = authorId;
    }

    public int AuthorId { get; set; }
}