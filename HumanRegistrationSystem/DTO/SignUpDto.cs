using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DTO;

public class SignUpDto
{
    [Required] public string UserName { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string PersonalId { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Surname { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] public string Email { get; set; }

    [Required] public AddressDto Address { get; set; }

    [Required] public IFormFile Picture { get; set; }
}