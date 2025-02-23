namespace Sample.Commons.Interfaces;

public interface IPageResult<T> where T : class
{
    public IEnumerable<T> Elements { get; init; }
    public int TotalElements { get; init; }
}
