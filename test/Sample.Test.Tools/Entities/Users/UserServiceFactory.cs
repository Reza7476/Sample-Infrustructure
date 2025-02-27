﻿using Sample.Application.Users;
using Sample.Application.Users.Services;
using Sample.Persistence.EF.DbContexts;
using Sample.Persistence.EF.Repositories.Users;

namespace Sample.Test.Tools.Entities.Users;

public static class UserServiceFactory
{
    public static IUserService Create(EFDataContext context)
    {
        var userRepository = new EFUserRepository(context);
        var unitOfWork = new EFUnitOfWork(context);

        return new UserAppService(userRepository, unitOfWork);
    }
}
