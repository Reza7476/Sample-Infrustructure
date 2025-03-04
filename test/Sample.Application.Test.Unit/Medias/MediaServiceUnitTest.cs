using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Sample.Application.Medias;
using Sample.Application.Medias.Dtos;
using Sample.Application.Medias.Exceptions;
using Sample.Application.Medias.Services;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Medias;
using Sample.Test.Tools.Entities.Medias;
using Sample.Test.Tools.Infrastructure.DataBaseConfig.Unit;
using System.Text;

using Sample.RestApi.Configs.Services.MediaServices;
namespace Sample.Application.Test.Unit.Medias;

public class MediaServiceUnitTest : BusinessUnitTest
{
    private readonly IMediaService _sut;
    private readonly Mock<IWebHostEnvironment> _mockIWebHostEnvironment;
    private readonly Mock<IFileSystem> _mockFileSystem;

    public MediaServiceUnitTest()
    {
        _mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
        _mockIWebHostEnvironment.Setup(env => env.WebRootPath).Returns("wwwroot");

        _mockFileSystem = new Mock<IFileSystem>();

        _sut = new MediaAppService(_mockIWebHostEnvironment.Object, _mockFileSystem.Object);

    }

    [Fact]
    public async Task CreateMediaModelFromFile_should_create_media_properly()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.jpg");
        mockFile.Setup(f => f.Length).Returns(500 * 1024);
        mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(Encoding.UTF8.GetBytes("test content")));
        var dto = new CreateMediaDto { File = mockFile.Object, Type = MediaType.Image };

        var result = await _sut.CreateMediaModelFromFile(dto);

        Assert.NotNull(result);
        Assert.Equal(MediaType.Image, result.Type);
        Assert.Equal(".jpg", result.Extension);
    }

    [Fact]
    public async Task CreateMediaModelFromFile_should_throw_exception_when_media_is_null()
    {
        var dto = new CreateMediaDto
        {
            File = null,
            Type = MediaType.Image
        };

        Func<Task> expected = async () => await _sut.CreateMediaModelFromFile(dto);

        await expected.Should().ThrowExactlyAsync<MediaIsNullException>();
    }

    [Fact]
    public async Task CreateMediaModelFromFile_should_throw_exception_when_file_size_is_not_acceptable()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.Length).Returns(11 * 1024 * 1024);
        var dto = new CreateMediaDto { File = mockFile.Object, Type = MediaType.Image };

        Func<Task> expected = async () => await _sut.CreateMediaModelFromFile(dto);

        await expected.Should().ThrowExactlyAsync<FileSizeNotAcceptableException>();
    }

    [Fact]
    public async Task CreateMediaModelFromFile_should_throw_exception_when_extension_not_acceptable()
    {
        var mockFile = new Mock<IFormFile>();
        mockFile.Setup(f => f.FileName).Returns("test.txt");
        mockFile.Setup(f => f.Length).Returns(500 * 1024);
        var dto = new CreateMediaDto { File = mockFile.Object, Type = MediaType.Image };

        Func<Task> expected = async () => await _sut.CreateMediaModelFromFile(dto);

        await expected.Should().ThrowAsync<FileExtensionNotAcceptableException>();
    }


    [Fact]
    public async Task SaveFileInHostFromBase64_ShouldCallFileOperations()
    {
        var base64Data = Convert.ToBase64String(new byte[] { 1, 2, 3, 4, 5 });
        var dto = new MediaBuilder()
        .WithUniqueName("name")
        .WithExtension(".jpg")
        .WithHash("has")
        .WithTitle("name")
        .WithThumbnailUniqueName("thumbname")
        .WithAltText("alt")
        .WithSortingOrder(1)
        .WithMainBase64(base64Data)
        .WithThumbBase64(base64Data)
        .WithUrl("/image/text/")
        .Build();

        _mockFileSystem.Setup(fs => fs.Exists(It.IsAny<string>())).Returns(false);

        await _sut.SaveFileInHostFromBase64(dto);

        var expectedUploadsFolder = Path.Combine("wwwroot/", dto.URL.TrimStart('/'));
        var expectedMainFilePath = Path.Combine(expectedUploadsFolder, dto.UniqueName + dto.Extension);
        _mockFileSystem.Verify(fs => fs.WriteAllBytesAsync(expectedMainFilePath, It.IsAny<byte[]>()), Times.Once);
        var expectedThumbFilePath = Path.Combine(expectedUploadsFolder, dto.ThumbnailUniqueName + dto.Extension);
        _mockFileSystem.Verify(fs => fs.WriteAllBytesAsync(expectedThumbFilePath, It.IsAny<byte[]>()), Times.Once);
    }

}





