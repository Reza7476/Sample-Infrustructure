using Sample.Application.Interfaces;
using Sample.Application.Medias.Dtos;
using Sample.Application.Medias.Services;
using Sample.Application.Users.Exceptions;
using Sample.Application.Users.Services;
using Sample.Commons.Exceptions;
using Sample.Core.Entities.Medias;

namespace Sample.Application.Users.UserHandlers;

public class UserCommandHandler : IUserHandler
{
    private readonly IUserService _userService;
    private readonly IMediaService _mediaService;
    private readonly IUnitOfWork _unitOfWork;

    public UserCommandHandler(
        IMediaService mediaService,
        IUserService userService,
        IUnitOfWork unitOfWork)
    {
        _mediaService = mediaService;
        _userService = userService;
        _unitOfWork = unitOfWork;
    }

    public async Task AddProfileImage(AddMediaDto dto, long userId)
    {
        await _unitOfWork.Begin();
        try
        {
            var user = await _unitOfWork.UserRepository
                .GetUserAndMediaById(userId) ?? throw new UserNotFoundException();

            if (!user.Medias.Any(_ => _.Type == MediaType.Image
                && _.TargetType == MediaTargetType.UserProfile_Image))
            {
                var mediaDto = new CreateMediaDto()
                {
                    Type = MediaType.Image,
                    TargetType = MediaTargetType.UserProfile_Image,
                    File = dto.File,
                    CreateThumbnail = true,
                };

                var image = await _mediaService
                    .CreateMediaModelFromFile(mediaDto) ?? throw new ErrorInCreateImageException();

                await _unitOfWork.MediaRepository.AddAsync(image);
                user.AddMedia(image);
                await _unitOfWork.Commit();

                await _mediaService.SaveFileInHostFromBase64(image);
            }
        }
        catch (Exception ex)
        {
            await _unitOfWork.RoleBack();
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateProfileImage(AddMediaDto dto, int userId)
    {
        await _unitOfWork.Begin();
        try
        {
            var user = await _unitOfWork.UserRepository
                .GetUserAndMediaById(userId) ?? throw new UserNotFoundException();

            if (user!.Medias.Any(_ => _.Type == MediaType.Image
                && _.TargetType == MediaTargetType.UserProfile_Image))
            {
                var mediaDto = new CreateMediaDto()
                {
                    Type = MediaType.Image,
                    TargetType = MediaTargetType.UserProfile_Image,
                    File = dto.File,
                    CreateThumbnail = true,
                    Media = user.Medias.First(_ => _.Type == MediaType.Image && 
                    _.TargetType == MediaTargetType.UserProfile_Image)
                };

                var image = await _mediaService.CreateMediaModelFromFile(mediaDto);

                _unitOfWork.MediaRepository.Update(image);

                await _mediaService.DeleteFileInHost(type: MediaType.Image,
                    image.TargetType, image.UniqueName);
                await _unitOfWork.Commit();
                await _mediaService.SaveFileInHostFromBase64(image);
            }
        }
        catch (Exception ex)
        {
            await _unitOfWork.RoleBack();
            throw new Exception(ex.Message);
        }
    }

}
