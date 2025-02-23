using Microsoft.EntityFrameworkCore;
using Sample.Commons.Interfaces;
using System.Linq.Expressions;
namespace Sample.Infrastructure.Queries;

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

static class PaginateExtensions
{
    internal static async Task<IPageResult<T>> Page<T>(
        this IQueryable<T> query,
        IPagination pagination)
        where T : class
    {
        var pageResult = await query
            .Skip(pagination.Offset!.Value * pagination.Limit!.Value)
            .Take(pagination.Limit.Value)
            .ToListAsyncSafe();

        return new PageResult<T>(pageResult, query.Count());
    }

    internal static async Task<IPageResult<T>> Page<T>(
        this IQueryable<T> query)
        where T : class
    {
        var pageResult = await query.ToListAsyncSafe();

        return new PageResult<T>(pageResult, pageResult.Count);
    }

    private static Task<List<TSource>> ToListAsyncSafe<TSource>(
        this IQueryable<TSource> query)
    {
        if (query is null)
            throw new ArgumentNullException(nameof(query));

        if (query is not IAsyncEnumerable<TSource>)
            return Task.FromResult(query.ToList());

        return query.ToListAsync();
    }
}

public static class Sorter
{
    public static IQueryable<T> SortQuery<T>(
        this IQueryable<T> query,
        ISort sort)
        where T : new()
    {
        foreach (var (propertyName, sortMethod) in sort.GetSortParameters())
        {
            query = query
                .CreateExpression(propertyName, sortMethod);
        }

        return query;
    }

    private static IQueryable<T> CreateExpression<T>(
        this IQueryable<T> query,
        string propertyName,
        string sortMethod)
        where T : new()
    {
        var parameter = Expression.Parameter(typeof(T));
        var property = Expression.Property(parameter, propertyName);
        var keySelector = Expression.Lambda(property, parameter);

        var expression = Expression.Call(
            typeof(Queryable),
            sortMethod,
            new[] { parameter.Type, property.Type },
            query.Expression,
            Expression.Quote(keySelector));

        return (IQueryable<T>)query.Provider.CreateQuery(expression);
    }
}


class PageResult<T> : IPageResult<T>
    where T : class
{
    public IEnumerable<T> Elements { get; init; }
    public int TotalElements { get; init; }

    public PageResult(IEnumerable<T> elements, int totalElements)
    {
        Elements = elements;
        TotalElements = totalElements;
    }
}

