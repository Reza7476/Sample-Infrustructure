using Sample.Application.Medias.Dtos;
using Sample.Application.Medias.Services;
using Sample.Application.Users.Exceptions;
using Sample.Application.Users.Services;
using Sample.Commons.Exceptions;
using Sample.Commons.UnitOfWork;
using Sample.Core.Entities.Medias;
using Sample.Core.Entities.Users;

namespace Sample.Application.Users.UserHandlers;

public class UserCommandHandler : IUserHandler
{
    private readonly IUserService _userService;
    private readonly IMediaService _mediaService;
    private readonly IUserRepository _repository;
    private readonly IMediaRepository _mediaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandHandler(
        IMediaService mediaService,
        IUserService userService,
        IUserRepository repository,
        IMediaRepository mediaRepository,
        IUnitOfWork unitOfWork)
    {
        _mediaService = mediaService;
        _userService = userService;
        _repository = repository;
        _mediaRepository = mediaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddProfileImage(AddMediaDto dto, long userId)
    {

        await _unitOfWork.Begin();
        try
        {
            var user = await _repository.GetUserAndMediaById(userId);
            StopIfUserNotFound(user);

            if (!user!.Medias
                .Any(_ => _.Type == MediaType.Image
                && _.TargetType == MediaTargetType.UserProfile_Image))
            {
                var mediaDto = new CreateMediaDto()
                {
                    Type = MediaType.Image,
                    TargetType = MediaTargetType.UserProfile_Image,
                    File = dto.File,
                    CreateThumbnail = true,
                };

                var image = await _mediaService.CreateMediaModelFromFile(mediaDto) ?? throw new ErrorInCreateImageException();

                user.AddMedia(image);
                await _mediaRepository.AddAsync(image);
                await _unitOfWork.Complete();

                await _mediaService.SaveFileInHostFromBase64(image);
            }
        }
        catch (Exception ex)
        {
            await _unitOfWork.RoleBack();
            throw new Exception(ex.Message);
        }
    }

    private static void StopIfUserNotFound(User? user)
    {
        if (user == null)
        {
            throw new UserNotFoundException();
        }
    }
}
