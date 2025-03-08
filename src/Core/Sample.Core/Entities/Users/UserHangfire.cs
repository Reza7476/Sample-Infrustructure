using Sample.Core.Entities.Generals;

namespace Sample.Core.Entities.Users;

public class UserHangfire : BaseEntity
{
    public long UserId { get; set; }
    public HnagFireStatus Status { get; set; }
    public User User { get; set; }

}

public enum HnagFireStatus : byte
{
    Ok = 1,
    Nok = 2
}