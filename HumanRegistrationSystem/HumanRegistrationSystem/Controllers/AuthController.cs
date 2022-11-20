using DTO;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using HumanRegistrationSystem_Domain;

namespace HumanRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserAccountService userAccountsService, IJwtService jwtService)
        {
            _userAccountService = userAccountsService;
            _jwtService = jwtService;
        }

        [HttpPost("Signup")]
        public async Task<ActionResult> SignUpAsync(SignUpDto signupDto)
        {
            //var image = _userAccountService.FileUpload(signupDto.Picture, 200, 200);

            //var success = await _userAccountService.CreateUserAccountAsync(signupDto, image.Result);

            //return success ? Ok() : BadRequest(new { ErrorMessage = "User already exist" });



            bool success;
            try
            {
                var image = await _userAccountService.FileUpload(signupDto.Picture, 200, 200);
                success = await _userAccountService.CreateUserAccountAsync(signupDto, image);
            }
            catch (NullReferenceException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            if (success)
            {
                return Ok();
            }
            return BadRequest(new { ErrorMessage = "User already exist" });

        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginDto loginDto)
        {
            bool loginSuccess;
            UserAccount account;
            try
            {
                (loginSuccess, account) =
                    await _userAccountService.LoginAsync(loginDto.UserName, loginDto.Password);
            }
            catch(NullReferenceException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            if (loginSuccess)
            {
                return Ok(_jwtService.GetJwtToken(account!));
            }

            return BadRequest(new { ErrorMessage = "Login failed" });
        }
    }
}
