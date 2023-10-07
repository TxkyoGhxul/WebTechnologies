using WebTechnologies.Application.Models;

namespace WebTechnologies.Application.Extensions;
public static class PagedListsExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> values, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await PagedList<T>.CreateAsync(values, pageNumber, pageSize, cancellationToken);
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> values, CancellationToken cancellationToken = default)
    {
        const int defaultPageNumber = 1;
        const int defaultPageSize = 10;

        return await values.ToPagedListAsync(defaultPageNumber, defaultPageSize, cancellationToken);
    }
}
