using Sample.Commons.Interfaces;

namespace Sample.Persistence.EF.Extensions.Paginations;

public class Sort<T> : ISort where T : new()
{
    private string? _expression;

    private Dictionary<string, string> _sortExtension;

    public Sort()
    {
        _sortExtension = new Dictionary<string, string>();
    }

    public string? Expression
    {
        get => Expression;
        set
        {
            _sortExtension = value.Parse<T>();
            _expression = value;
        }
    }
    public Dictionary<string, string> GetSortParameters() => _sortExtension;

}
