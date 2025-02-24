namespace Sample.Core.Entities.Generals;

public class BaseEntity
{
    public int Id { get; set; }
    public long CreatedAt { get; set; } //= DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    public long? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
