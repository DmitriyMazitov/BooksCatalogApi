using Test.Work.BooksCatalogApi.Contracts.Enums;

namespace Test.Work.BooksCatalogApi.Contracts.Requests.AuthorsRequests.GetAuthors;


/// <summary>
/// Запрос на получение списка авторов
/// </summary>
public class GetAuthorsRequest
{
    private int _pageNumber;
    private int _pageSize;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetAuthorsRequest(GetAuthorsRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        IsAscending = request.IsAscending;
        _pageNumber = request.PageNumber;
        _pageSize = request.PageSize;
        OrderBy = request.OrderBy;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetAuthorsRequest()
    {
        _pageNumber = PaginationDefaults.PageNumber;
        _pageSize = PaginationDefaults.PageSize;
    }

    /// <summary>
    /// Номер страницы, начиная с 1-ой
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value > 0 ? value : PaginationDefaults.PageNumber;
    }

    /// <summary>
    /// Размер страницы
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0 ? value : PaginationDefaults.PageSize;
    }

    /// <summary>
    /// Поле сортировки
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// Сортировка по возрастанию
    /// </summary>
    public bool IsAscending { get; set; }
}