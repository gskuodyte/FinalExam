using HumanRegistrationSystem.Dto;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HumanRegistrationSystem.Controllers
{
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

        [HttpGet("{id}")]

        public ActionResult<HumanInfoDto> GetUserById(int id)
        {

            var existingUser = _userAccountService.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            };

            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public  ActionResult DeleteUserById(int id)
        {
            var existingUser = _userAccountService.GetUserById(id).Result;
            if (existingUser == null)
            {
                return NotFound();
            };
            _userAccountService.DeleteUser(existingUser);
            return Ok();
        }
    }
}
