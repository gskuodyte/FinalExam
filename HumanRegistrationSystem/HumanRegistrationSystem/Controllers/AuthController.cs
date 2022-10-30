
using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Mvc;

namespace HumanRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IJwtService _jwtService;
        private readonly IImagesService _imagesService;

        public AuthController(IUserAccountService userAccountsService, IJwtService jwtService, IImagesService imagesService)
        {
            _userAccountService = userAccountsService;
            _jwtService = jwtService;
            _imagesService = imagesService;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup(SignUpDto signupDto)
        {
            using var memoryStream = new MemoryStream();
            signupDto.Human.Image.CopyTo(memoryStream);
            var imageBytes = memoryStream.ToArray();

            var savedImage = await _imagesService.AddImageAsync(imageBytes, signupDto.Human.Image.ContentType);

            var success = await _userAccountService.CreateUserAccountAsync(signupDto.UserName, signupDto.Password, signupDto.Human, signupDto.Human.Address, savedImage);

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
