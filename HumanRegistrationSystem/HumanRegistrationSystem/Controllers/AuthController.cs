
using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Signup(SignUpDto signupDto)
        {
            var humanInfoDto = new HumanInfoDto();
            humanInfoDto.SetProfilePicture(signupDto.Picture);

            var success = await _userAccountService.CreateUserAccountAsync(signupDto, humanInfoDto.Picture);

            return success ? Ok() : BadRequest(new { ErrorMessage = "User already exist" });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var (loginSuccess, account) = await _userAccountService.LoginAsync(loginDto.UserName, loginDto.Password);

            if (loginSuccess)
            {
                return Ok(_jwtService.GetJwtToken(account));
            }
            else
            {
                return BadRequest(new { ErrorMessage = "Login failed" });
            }
        }
    }
}
