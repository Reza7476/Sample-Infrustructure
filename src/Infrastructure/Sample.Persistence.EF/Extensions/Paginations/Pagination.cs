using Sample.Commons.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Sample.Persistence.EF.Extensions.Paginations;

public class Pagination : IPagination
{
    [Range(0, int.MaxValue)]
    public int? Offset { get; set; }
    [Range(0, int.MaxValue)]
    public int? Limit { get; set; }
}
