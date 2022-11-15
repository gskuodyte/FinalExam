using System.Drawing;
using Common.Validation;
using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using System.Xml.Linq;

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
            var responce = Validation.CheckIfNull(personalId);
            if (responce!)
            {
                await _userAccountService.UpdateUserPersonalId(ClaimId(), personalId);
                return Ok();
            }
            return BadRequest("Input was null! try again");

        }

        [HttpPut("Name")]
        public async Task<ActionResult> UpdateHumanName([FromQuery] string name)
        {
            var responce = Validation.CheckIfNull(name);
            if (responce!)
            {
                _userAccountService.UpdateUserName(ClaimId(), name);
                return Ok();
            }
            return BadRequest("Input was null! try again");

        }

        [HttpPut("Surname")]
        public async Task<ActionResult> UpdateHumanSurname([FromQuery] string surname)
        {
            var responce = Validation.CheckIfNull(surname);
            if (responce!)
            {
                _userAccountService.UpdateUserSurname(ClaimId(), surname);
                return Ok();
            }
            return BadRequest("Input was null! try again");

        }

        [HttpPut("PhoneNumber")]
        public async Task<ActionResult> UpdateHumanPhoneNumber([FromQuery] string phoneNumber)
        {
            var responce = Validation.CheckIfNull(phoneNumber);
            if (responce!)
            {
                _userAccountService.UpdateUserPhoneNumber(ClaimId(), phoneNumber);
                return Ok();
            }
            return BadRequest("Input was null! try again");

        }

        [HttpPut("Email")]
        public async Task<ActionResult> UpdateHumanEmail([FromQuery] string email)
        {
            var responce = Validation.CheckIfNull(email);
            if (responce!)
            {
                _userAccountService.UpdateUserEmail(ClaimId(), email);
                return Ok();
            }
            return BadRequest("Input was null! try again");

        }

        [HttpPut("Image")]
        public async Task<ActionResult> UpdateHumanImage([FromForm] ImageUploadRequest request)
        {
            var humanInfoDto = new HumanInfoDto();
            var image = _userAccountService.FileUpload(request.Image, 200, 200);

            await _userAccountService.UpdateImageAsync(ClaimId(), image.Result);

            return Ok();
        }

        [HttpPut("City")]
        public async Task<ActionResult> UpdateHumanCityAddress([FromQuery] string city)
        {
            var responce = Validation.CheckIfNull(city);
            if (responce!)
            {
                _userAccountService.UpdateUserCityAddress(ClaimId(), city);
                return Ok();
            }
            return BadRequest("Input was null! try again");


        }

        [HttpPut("Street")]
        public async Task<ActionResult> UpdateHumanStreetAddress([FromQuery] string street)
        {
            var responce = Validation.CheckIfNull(street);
            if (responce!)
            {
                _userAccountService.UpdateUserStreetAddress(ClaimId(), street);
                return Ok();
            }
            return BadRequest("Input was null! try again");

        }

        [HttpPut("HouseNumber")]
        public async Task<ActionResult> UpdateHumanHouseNumberAddress([FromQuery] int houseNumber)
        {
            var responce = Validation.CheckIfNull(houseNumber);
            if (responce!)
            {
                _userAccountService.UpdateUserHouseNumberAddress(ClaimId(), houseNumber);
                return Ok();
            }
            return BadRequest("Input was null! try again");
        }

        [HttpPut("ApartmentNumber")]
        public async Task<ActionResult> UpdateHumanApartmentNumberAddress([FromQuery] int apartmentNumber)
        {
            var responce = Validation.CheckIfNull(apartmentNumber);
            if (responce!)
            {
                _userAccountService.UpdateUserApartmentNumberAddress(ClaimId(), apartmentNumber);
                return Ok();
            }
            return BadRequest("Input was null! try again");

        }

        
    }
}
