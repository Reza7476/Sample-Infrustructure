using Sample.Application.Users;
using Sample.Application.Users.Services;
using Sample.Persistence.EF.DbContexts;
using Sample.Test.Tools.Infrastructure;

namespace Sample.Test.Tools.Entities.Users;

public static class UserServiceFactory
{
    public static IUserService Create(EFDataContext context)
    {
        var fakeUnitOfWork = new EFUnitOfWork(context);

        return new UserAppService(fakeUnitOfWork);
    }
}
