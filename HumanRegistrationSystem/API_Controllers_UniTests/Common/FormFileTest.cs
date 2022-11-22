using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace RegistrationSystem.Controllers.DTOs
{
    [SupportedOSPlatform("windows")]
    public class FormFileTest : IFormFile
    {
        public string ContentType => "jpg";

        public string ContentDisposition => "";

        public IHeaderDictionary Headers => throw new NotImplementedException( );

        private long _maxFileSize = 1024 * 1024;
        public long Length { get => _maxFileSize; set => _maxFileSize = value; }

        private string _name = "test.jpg";
        public string Name { get => _name; set => _name = value; }

        private string _fileName = "test.jpg";
        public string FileName { get => _fileName; set => _fileName = value; }

        public void CopyTo (Stream target)
        {
            var image = new Bitmap(300, 300);
            image.Save(target, ImageFormat.Png);
        }

        public Task CopyToAsync (Stream target, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException( );
        }

        public Stream OpenReadStream ( )
        {
            throw new NotImplementedException( );
        }
    }
}
