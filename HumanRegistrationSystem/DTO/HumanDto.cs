using Microsoft.AspNetCore.Http;

namespace HumanRegistrationSystem.Dto
{
    public class HumanDto
    {
        public int PersonalID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int TelephoneNumber { get; set; }
        public string Email { get; set; }
        public IFormFile Image { get; set; }
        public AddressDto Address { get; set; }
    }
}
