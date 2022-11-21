using Common.Validation;
using Microsoft.AspNetCore.Http;

namespace DTO;

public class ImageUploadRequest
{
    [MaxFileSize(5 * 200 * 200)]
    [AllowedExtensions(new[] { ".png", ".jpg" })]
    public IFormFile Image { get; set; }
}