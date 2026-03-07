using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DotNetWithKafka.Infrastructure.Extensions;

public static class PagedListExtensions
{
    public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        if (count > 0)
        {
            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);
            return new StaticPagedList<T>(items, pageNumber, pageSize, count);
        }

        return new StaticPagedList<T>(new List<T>(), pageNumber, pageSize, 0);
    }
}
