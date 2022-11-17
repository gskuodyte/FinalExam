using Microsoft.AspNetCore.Http;

namespace DTO
{
    public class SignUpDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PersonalID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
        public IFormFile Picture { get; set; }
    }
}
