namespace Sample.Commons.Interfaces;

public interface IPagination
{
    int? Offset { get; set; }
    int? Limit { get; set; }
}
