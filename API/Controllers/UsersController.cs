using Microsoft.AspNetCore.Mvc;

using API.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult<UserReadDto> GetUserId()
        {
            var userReadDto = new UserReadDto(User.FindFirstValue(ClaimTypes.NameIdentifier), User.FindFirstValue(ClaimTypes.Email), User.FindFirstValue(ClaimTypes.Role));
            return Ok(userReadDto);
        }

        [AllowAnonymous]
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