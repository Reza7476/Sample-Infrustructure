using System.Reflection;
using System.Text;

namespace Sample.Persistence.EF.Extensions.Paginations;


public static class SortParser
{
    private const char SplitCharachter = ',';
    private const char AscendingPrefix = '+';
    private const char DescendingPrefix = '-';

    public static Dictionary<string, string> Parse<T>(
        this string? expression)
        where T : new()
    {
        var result = new Dictionary<string, string>();
        var args = expression
                       ?.Replace(" ", string.Empty)
                       .Split(SplitCharachter)
                   ?? Array.Empty<string>();

        var i = 0;
        foreach (var arg in args)
        {
            var isArgFirst = i is 0;
            var propertyName = arg.GetProperty<T>();
            var sortMethod = arg.GetSortMethod(isArgFirst);
            i++;

            result.Add(propertyName, sortMethod);
        }

        return result;
    }

    private static string GetProperty<T>(this string arg)
        where T : new()
    {
        var bindingFlags = BindingFlags.Public
                           | BindingFlags.Instance
                           | BindingFlags.IgnoreCase;

        var propName = arg.Trim(AscendingPrefix, DescendingPrefix);
        var propertyInfo = typeof(T)?.GetProperty(propName, bindingFlags);

        if (propertyInfo is null)
            throw new ArgumentException($"Invalid property '{propName}'.");

        return propertyInfo.Name;
    }

    private static string GetSortMethod(
        this string arg, bool isArgFirst)
    {
        var prefix = arg.First();

        if (prefix is not AscendingPrefix and not DescendingPrefix)
            throw new ArgumentException($"Invalid prefix '{prefix}'.");

        return new StringBuilder()
            .Append(isArgFirst ? "OrderBy" : "ThenBy")
            .Append(prefix
                .Equals(DescendingPrefix)
                ? "Descending" : string.Empty)
            .ToString();
    }

    public static IEnumerable<Tuple<string, string>> GetSortArguments(
        this string? expression)
    {
        var args = expression
                       ?.Trim().Split(SplitCharachter)
                   ?? Array.Empty<string>();

        var list = new List<Tuple<string, string>>();

        foreach (var arg in args)
        {
            var property = arg.Trim(AscendingPrefix, DescendingPrefix);
            var sortMethod = arg.GetSortMethod();

            list.Add(Tuple.Create(property, sortMethod));
        }

        return list;
    }

    private static string GetSortMethod(this string sortArg)
    {
        var prefix = sortArg.First();

        var method = "ThenBy"
                     + (prefix == DescendingPrefix ? "Descending" : null);

        return method;
    }
}