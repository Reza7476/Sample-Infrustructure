using Sample.Commons.Interfaces;

namespace Sample.Persistence.EF.Extensions.Paginations;

public static class QueryExtensions
{
    public static async Task<IPageResult<T>> Paginate<T>(
        this IQueryable<T> query,
        IPagination pagination)
        where T : class
    {
        if (pagination.Limit.HasValue && pagination.Offset.HasValue)
            return await query.Page(pagination);

        return await query.Page();
    }

    public static IQueryable<T> Sort<T>(
        this IQueryable<T> query,
        ISort expression)
        where T : new()
    {
        return query.SortQuery(expression);
    }
}