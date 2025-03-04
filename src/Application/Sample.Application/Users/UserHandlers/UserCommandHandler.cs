using Sample.Application.Medias.Dtos;
using Sample.Application.Medias.Services;
using Sample.Application.Users.Dtos;
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
    private readonly IUserRepository _userRepository;
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
        _userRepository = repository;
        _mediaRepository = mediaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddProfileImage(AddMediaDto dto, long userId)
    {
        await _unitOfWork.Begin();
        try
        {
            var user = await _userRepository.GetUserAndMediaById(userId) ?? throw new UserNotFoundException(); 

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

                var image = await _mediaService.CreateMediaModelFromFile(mediaDto) ?? throw new ErrorInCreateImageException();

                await _mediaRepository.AddAsync(image);
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
            var user = await _userRepository.GetUserAndMediaById(userId)?? throw new UserNotFoundException();

            if (user!.Medias.Any(_ => _.Type == MediaType.Image
                && _.TargetType == MediaTargetType.UserProfile_Image))
            {
                var mediaDto = new CreateMediaDto()
                {
                    Type = MediaType.Image,
                    TargetType = MediaTargetType.UserProfile_Image,
                    File = dto.File,
                    CreateThumbnail = true,
                    Media = user.Medias.First(_ => _.Type == MediaType.Image && _.TargetType == MediaTargetType.UserProfile_Image)
                };

                var image = await _mediaService.CreateMediaModelFromFile(mediaDto);

                _mediaRepository.Update(image);

                await _mediaService.DeleteFileInHost(type: MediaType.Image, image.TargetType, image.UniqueName);
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
