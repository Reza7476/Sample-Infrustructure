namespace Sample.Commons.Interfaces;

public interface IUserTokenService : IScope
{
    string? Mac_Id { get; }
    int? UserId { get; }

}
