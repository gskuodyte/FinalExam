using Common.Validation;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DTO;

namespace HumanRegistrationSystem.Controllers
{
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
            if (Validation.CheckIfNull(claim))
            {
                return 0;
            }
            return claim;
        }


        [HttpPut("PersonalId")]
        public async Task<ActionResult> UpdateHumanPersonalId([FromQuery] int personalId)
        {
            if (Validation.CheckIfNull(personalId))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserPersonalId(ClaimId(), personalId);
            return Ok();
        }

        [HttpPut("Name")]
        public async Task<ActionResult> UpdateHumanName([FromQuery] string name)
        {
            if (Validation.CheckIfNull(name))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserName(ClaimId(), name);
            return Ok();
        }

        [HttpPut("Surname")]
        public async Task<ActionResult> UpdateHumanSurname([FromQuery] string surname)
        {
            if (Validation.CheckIfNull(surname))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserSurname(ClaimId(), surname);
            return Ok();
        }

        [HttpPut("PhoneNumber")]
        public async Task<ActionResult> UpdateHumanPhoneNumber([FromQuery] string phoneNumber)
        {
            if (Validation.CheckIfNull(phoneNumber))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserPhoneNumber(ClaimId(), phoneNumber);
            return Ok();

        }

        [HttpPut("Email")]
        public async Task<ActionResult> UpdateHumanEmail([FromQuery] string email)
        {
            if (Validation.CheckIfNull(email))
            {
                return BadRequest("Input was null! try again");
            }
           
            await _userAccountService.UpdateUserEmail(ClaimId(), email);
            return Ok();
        }

        [HttpPut("Image")]
        public async Task<ActionResult> UpdateHumanImage([FromForm] ImageUploadRequest request)
        {
            var image = await _userAccountService.FileUpload(request.Image, 200, 200);

            await _userAccountService.UpdateImageAsync(ClaimId(), image);

            return Ok();
        }

        [HttpPut("City")]
        public async Task<ActionResult> UpdateHumanCityAddress([FromQuery] string city)
        {
            if (Validation.CheckIfNull(city))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserCityAddress(ClaimId(), city);
            return Ok();
        }

        [HttpPut("Street")]
        public async Task<ActionResult> UpdateHumanStreetAddress([FromQuery] string street)
        {
            if (Validation.CheckIfNull(street))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserStreetAddress(ClaimId(), street);
            return Ok();
        }

        [HttpPut("HouseNumber")]
        public async Task<ActionResult> UpdateHumanHouseNumberAddress([FromQuery] int houseNumber)
        {
            if (Validation.CheckIfNull(houseNumber))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserHouseNumberAddress(ClaimId(), houseNumber);
            return Ok();
        }

        [HttpPut("ApartmentNumber")]
        public async Task<ActionResult> UpdateHumanApartmentNumberAddress([FromQuery] int apartmentNumber)
        {
            if (Validation.CheckIfNull(apartmentNumber))
            {
                return BadRequest("Input was null! try again");
            }
            await _userAccountService.UpdateUserApartmentNumberAddress(ClaimId(), apartmentNumber);
            return Ok();
        }

        
    }
}
