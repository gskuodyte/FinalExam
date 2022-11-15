using System.Drawing;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace HumanRegistrationSystem.Dto
{
    public class HumanInfoDto
    {
        public int PersonalID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public byte[] Picture { get; set; }
        public string? ContentType { get; set; }
       
        public AddressDto Address { get; set; }

        public HumanInfoDto() { }

        public void SetProfilePicture (IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            Picture = memoryStream.ToArray();
            ContentType = file.ContentType;
        }
    }
}
