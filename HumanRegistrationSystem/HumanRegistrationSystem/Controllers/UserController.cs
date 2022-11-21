using System.Net;
using System.Security.Claims;
using Common.Validation;
using DTO;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanRegistrationSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "User")]
public class UserController : ControllerBase
{
    private readonly IUserAccountService _userAccountService;

    public UserController(IUserAccountService userAccountsService)
    {
        _userAccountService = userAccountsService;
    }

    private int ClaimId()
    {
        var claim = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        if (Validation.CheckIfNull(claim)) return 0;
        return claim;
    }


    [HttpPut("PersonalId")]
    public async Task<ActionResult> UpdateHumanPersonalIdAsync([FromQuery] string personalId)
    {
        if (string.IsNullOrEmpty(personalId)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserPersonalIdAsync(ClaimId(), personalId);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("Name")]
    public async Task<ActionResult> UpdateHumanNameAsync([FromQuery] string name)
    {
        if (Validation.CheckIfNull(name)) return BadRequest("Input was null! try again");
        await _userAccountService.UpdateUserNameAsync(ClaimId(), name);
        return Ok();
    }

    [HttpPut("Surname")]
    public async Task<ActionResult> UpdateHumanSurnameAsync([FromQuery] string surname)
    {
        if (Validation.CheckIfNull(surname)) return BadRequest("Input was null! try again");
        await _userAccountService.UpdateUserSurnameAsync(ClaimId(), surname);
        return Ok();
    }

    [HttpPut("PhoneNumber")]
    public async Task<ActionResult> UpdateHumanPhoneNumberAsync([FromQuery] string phoneNumber)
    {
        if (Validation.CheckIfNull(phoneNumber)) return BadRequest("Input was null! try again");
        await _userAccountService.UpdateUserPhoneNumberAsync(ClaimId(), phoneNumber);
        return Ok();
    }

    [HttpPut("Email")]
    public async Task<ActionResult> UpdateHumanEmailAsync([FromQuery] string email)
    {
        if (Validation.CheckIfNull(email)) return BadRequest("Input was null! try again");

        await _userAccountService.UpdateUserEmailAsync(ClaimId(), email);
        return Ok();
    }

    [HttpPut("Image")]
    public async Task<ActionResult> UpdateHumanImageAsync([FromForm] ImageUploadRequest request)
    {
        var image = await _userAccountService.FileUploadAsync(request.Image, 200, 200);

        await _userAccountService.UpdateImageAsync(ClaimId(), image);

        return Ok();
    }

    [HttpPut("City")]
    public async Task<ActionResult> UpdateHumanCityAddressAsync([FromQuery] string city)
    {
        if (Validation.CheckIfNull(city)) return BadRequest("Input was null! try again");
        await _userAccountService.UpdateUserCityAddressAsync(ClaimId(), city);
        return Ok();
    }

    [HttpPut("Street")]
    public async Task<ActionResult> UpdateHumanStreetAddressAsync([FromQuery] string street)
    {
        if (Validation.CheckIfNull(street)) return BadRequest("Input was null! try again");
        await _userAccountService.UpdateUserStreetAddressAsync(ClaimId(), street);
        return Ok();
    }

    [HttpPut("HouseNumber")]
    public async Task<ActionResult> UpdateHumanHouseNumberAddressAsync([FromQuery] int houseNumber)
    {
        if (Validation.CheckIfNull(houseNumber)) return BadRequest("Input was null! try again");
        await _userAccountService.UpdateUserHouseNumberAddressAsync(ClaimId(), houseNumber);
        return Ok();
    }

    [HttpPut("ApartmentNumber")]
    public async Task<ActionResult> UpdateHumanApartmentNumberAddressAsync([FromQuery] int apartmentNumber)
    {
        if (Validation.CheckIfNull(apartmentNumber)) return BadRequest("Input was null! try again");
        await _userAccountService.UpdateUserApartmentNumberAddressAsync(ClaimId(), apartmentNumber);
        return Ok();
    }
}