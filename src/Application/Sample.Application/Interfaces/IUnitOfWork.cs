using Sample.Application.Medias.Services;
using Sample.Application.Users.Services;
using Sample.Commons.Interfaces;

namespace Sample.Application.Interfaces;

public interface IUnitOfWork : IScope
{
    Task Begin();
    Task Commit();
    Task Complete();
    Task RoleBack();

    IUserRepository UserRepository { get; }

    IMediaRepository MediaRepository { get; }

}
