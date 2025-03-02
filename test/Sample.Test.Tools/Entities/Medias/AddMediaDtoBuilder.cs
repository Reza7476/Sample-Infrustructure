using Microsoft.AspNetCore.Http;
using Moq;
using Sample.Application.Medias.Dtos;

namespace Sample.Test.Tools.Entities.Medias;

public class AddMediaDtoBuilder
{
    private readonly AddMediaDto _dto;

    public AddMediaDtoBuilder()
    {
        _dto = new AddMediaDto()
        {
            File = CreateMockFile() // Generate a mock file
        };
    }

    public AddMediaDtoBuilder WithFile()
    {
        _dto.File = CreateMockFile();
        return this;
    }

    public AddMediaDto Build()
    {
        return _dto;
    }

    private IFormFile CreateMockFile()
    {
        var mockFile = new Mock<IFormFile>();

        var fileName = "test-file.jpg";
        var content = "Fake file content";
        var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(contentBytes);

        mockFile.Setup(f => f.FileName).Returns(fileName);
        mockFile.Setup(f => f.Length).Returns(contentBytes.Length);
        mockFile.Setup(f => f.OpenReadStream()).Returns(stream);
        mockFile.Setup(f => f.ContentType).Returns("image/jpeg");

        return mockFile.Object;
    }
}
