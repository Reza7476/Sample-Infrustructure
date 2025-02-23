namespace Sample.Commons.Interfaces;

public interface ISort
{
    string? Expression { get; }

    Dictionary<string, string> GetSortParameters();
}
