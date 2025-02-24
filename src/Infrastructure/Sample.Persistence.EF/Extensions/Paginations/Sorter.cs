using Sample.Commons.Interfaces;
using System.Linq.Expressions;

namespace Sample.Persistence.EF.Extensions.Paginations;


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

