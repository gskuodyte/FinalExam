using System.IO;
using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
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
        if (string.IsNullOrEmpty(name)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserNameAsync(ClaimId(), name);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("Surname")]
    public async Task<ActionResult> UpdateHumanSurnameAsync([FromQuery] string surname)
    {
        if (string.IsNullOrEmpty(surname)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserSurnameAsync(ClaimId(), surname);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("PhoneNumber")]
    public async Task<ActionResult> UpdateHumanPhoneNumberAsync([FromQuery] string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserPhoneNumberAsync(ClaimId(), phoneNumber);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("Email")]
    public async Task<ActionResult> UpdateHumanEmailAsync([FromQuery] string email)
    {
        if (string.IsNullOrEmpty(email)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserEmailAsync(ClaimId(), email);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("Image")]
    public async Task<ActionResult> UpdateHumanImageAsync([FromForm] ImageUploadRequest request)
    {
        bool result;
        try
        {
            var image = await _userAccountService.FileUploadAsync(request.Image, 200, 200);
            result = await _userAccountService.UpdateImageAsync(ClaimId(), image);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("City")]
    public async Task<ActionResult> UpdateHumanCityAddressAsync([FromQuery] string city)
    {
        if (string.IsNullOrEmpty(city)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserCityAddressAsync(ClaimId(), city);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("Street")]
    public async Task<ActionResult> UpdateHumanStreetAddressAsync([FromQuery] string street)
    {
        if (string.IsNullOrEmpty(street)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserStreetAddressAsync(ClaimId(), street);
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("HouseNumber")]
    public async Task<ActionResult> UpdateHumanHouseNumberAddressAsync([FromQuery] string houseNumber)
    {
        if (string.IsNullOrEmpty(houseNumber)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserHouseNumberAddressAsync(ClaimId(), int.Parse(houseNumber));
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }

    [HttpPut("ApartmentNumber")]
    public async Task<ActionResult> UpdateHumanApartmentNumberAddressAsync([FromQuery] string apartmentNumber)
    {
        if (string.IsNullOrEmpty(apartmentNumber)) return BadRequest("Input was null! try again");

        bool result;
        try
        {
            result = await _userAccountService.UpdateUserApartmentNumberAddressAsync(ClaimId(), int.Parse(apartmentNumber)); ;
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (result) return Ok();
        return StatusCode((int)HttpStatusCode.InternalServerError);
    }
}