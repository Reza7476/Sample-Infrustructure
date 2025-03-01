using System.ComponentModel.DataAnnotations;

namespace Sample.Core.Entities.Generals;

public class BaseEntity
{
    public long Id { get; set; }
    public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long? CreatedBy { get; set; }
    public long? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
}
