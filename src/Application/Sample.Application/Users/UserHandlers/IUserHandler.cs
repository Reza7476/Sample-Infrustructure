using Sample.Application.Medias.Dtos;
using Sample.Commons.Interfaces;

namespace Sample.Application.Users.UserHandlers;

public interface IUserHandler : IScope
{
    Task AddProfileImage(AddMediaDto dto, long userId);
}
