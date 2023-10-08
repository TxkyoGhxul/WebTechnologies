using Microsoft.EntityFrameworkCore;

namespace WebTechnologies.Application.Models;
public class PagedList<T>
{
    private const int _defaultPageSize = 10;
    private const int _defaultPageNumber = 10;

    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }

    private PagedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        SetDefaultPageSizeIfNonPositive(ref pageSize);
        SetDefaultPageNumberIfNonPositive(ref pageNumber);

        var count = await source.CountAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new(items, count, pageNumber, pageSize);
    }

    private static void SetDefaultPageSizeIfNonPositive(ref int pageSize)
    {
        if (pageSize <= 0)
        {
            pageSize = _defaultPageSize;
        }
    }

    private static void SetDefaultPageNumberIfNonPositive(ref int pageNumber)
    {
        if (pageNumber <= 0)
        {
            pageNumber = _defaultPageNumber;
        }
    }
}
