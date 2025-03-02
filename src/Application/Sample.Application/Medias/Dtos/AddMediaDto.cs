using Microsoft.AspNetCore.Http;

namespace Sample.Application.Medias.Dtos;

public class AddMediaDto
{
    public required IFormFile File { get; set; }
}
