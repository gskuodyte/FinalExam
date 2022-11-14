using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HumanRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IJwtService _jwtService;


        public UserController(IUserAccountService userAccountsService, IJwtService jwtService)
        {
            _userAccountService = userAccountsService;
            _jwtService = jwtService;

        }

        private int ClaimId()
        {
            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return id;
        }


        [HttpPut("PersonalId")]
        public async Task<ActionResult> UpdateHumanPersonalId([FromQuery] int personalId)
        {
            await _userAccountService.UpdateUserPersonalId(ClaimId(), personalId);
            return Ok();
        }

        [HttpPut("Name")]
        public async Task<ActionResult> UpdateHumanName([FromQuery] string name)
        {
            _userAccountService.UpdateUserName(ClaimId(), name);
            return Ok();
        }

        [HttpPut("Surname")]
        public async Task<ActionResult> UpdateHumanSurname([FromQuery] string surname)
        {
            _userAccountService.UpdateUserSurname(ClaimId(), surname);
            return Ok();
        }

        [HttpPut("PhoneNumber")]
        public async Task<ActionResult> UpdateHumanPhoneNumber([FromQuery] string phoneNumber)
        {
            _userAccountService.UpdateUserPhoneNumber(ClaimId(), phoneNumber);
            return Ok();
        }

        [HttpPut("Email")]
        public async Task<ActionResult> UpdateHumanEmail([FromQuery] string email)
        {
            _userAccountService.UpdateUserEmail(ClaimId(), email);
            return Ok();
        }

        [HttpPut("Image")]
        public async Task<ActionResult> UpdateHumanImage([FromForm] ImageUploadRequest request)
        {

            var humanInfoDto = new HumanInfoDto();
            humanInfoDto.SetProfilePicture(request.Image);

            await _userAccountService.UpdateImageAsync(ClaimId(), humanInfoDto.Picture);
            
            return Ok();
        }

        [HttpPut("City")]
        public async Task<ActionResult> UpdateHumanCityAddress([FromQuery] string city)
        {
            _userAccountService.UpdateUserCityAddress(ClaimId(), city);
            return Ok();
        }

        [HttpPut("Street")]
        public async Task<ActionResult> UpdateHumanStreetAddress([FromQuery] string street)
        {
            _userAccountService.UpdateUserStreetAddress(ClaimId(), street);
            return Ok();
        }

        [HttpPut("HouseNumber")]
        public async Task<ActionResult> UpdateHumanHouseNumberAddress([FromQuery] int houseNumber)
        {
            _userAccountService.UpdateUserHouseNumberAddress(ClaimId(), houseNumber);
            return Ok();
        }

        [HttpPut("ApartmentNumber")]
        public async Task<ActionResult> UpdateHumanApartmentNumberAddress([FromQuery] int apartmentNumber)
        {
            _userAccountService.UpdateUserApartmentNumberAddress(ClaimId(), apartmentNumber);
            return Ok();
        }
    }
}
