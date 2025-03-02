namespace Sample.Commons.Interfaces;

public interface ICurrentUserService : IScope
{
    long? UserId { get; }
    int GetCurrentUserId();
}
