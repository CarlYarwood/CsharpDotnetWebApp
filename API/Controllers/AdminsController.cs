using Microsoft.AspNetCore.Mvc;

using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Admin")]
    public class AdminsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            var email = registerUserDto.Email;
            if (await _userManager.FindByEmailAsync(email) != null)
            {
                return ValidationProblem("User already exists for passed email");
            }
            var user = new IdentityUser();
            user.UserName = email;
            user.Email = email;
            await _userManager.CreateAsync(user, registerUserDto.Password);
            await _userManager.AddToRoleAsync(user, "User");
            return Ok();
        }
    }
}