using Common.Validation;
using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class ImageUploadRequest
    {
        [MaxFileSize(5 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".png", ".jpg" })]
        public IFormFile Image { get; set; }

    }
}
