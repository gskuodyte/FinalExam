using Common.Validation;
using DTO;
using HumanRegistrationSystem_BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<ActionResult<UserAccountInfoResponce>> GetUserById(int id)
        {

            var existingUser = await _userAccountService.GetMapedUserAccount(id);
            
            if (Validation.CheckIfNull(existingUser))
            {
                return NotFound();
            }


            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserById(int id)
        {
            if (await _userAccountService.DeleteUser(id))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
