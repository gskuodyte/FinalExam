using System.Net;
using DTO;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanRegistrationSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IUserAccountService _userAccountService;

    public AdminController(IUserAccountService userAccountsService)
    {
        _userAccountService = userAccountsService;
    }

    [HttpGet("User{id}")]
    public async Task<ActionResult<UserAccountInfoResponce>> GetUserById(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest("Id can not be null or empty");

        UserAccountInfoResponce existingUser;
        try
        {
            existingUser = await _userAccountService.GetMappedUserAccountAsync(int.Parse(id));
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return Ok(existingUser);
    }

    [HttpGet("User_Picture")]
    public async Task<ActionResult> GetUserImage([FromQuery] string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest("Id can not be null or empty");

        var user = await _userAccountService.GetMappedUserAccountAsync(int.Parse(id));

        return File(user.Picture, "image/jpeg");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserById(string id)
    {
        if (string.IsNullOrEmpty(id)) return BadRequest("Id can not be null or empty");
        try
        {
            await _userAccountService.DeleteUserAsync(int.Parse(id));
        }
        catch (NullReferenceException)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return Ok();
    }
}