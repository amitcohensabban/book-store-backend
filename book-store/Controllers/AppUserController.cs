using book_store.Models;
using book_store.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace book_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;

        public AppUserController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        [HttpPost("signUp")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var res = await _appUserRepository.SignUp(signupModel);
            if (res.Succeeded)
            {
                return Ok(res.Succeeded);
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel signinModel)
        {
            var res = await _appUserRepository.Login(signinModel);
            if (string.IsNullOrWhiteSpace(res))
            {
                return Unauthorized();
            }
            var tokenResponse = new { token = res };
            return Ok(tokenResponse);
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromQuery]string userId)
        {
            var res =  await _appUserRepository.DeleteUserAsync(userId);
            if(res == null)
            {
                return BadRequest("user not found");
            }
            return Ok("user deleted");
        }

        [HttpPatch ("updateDetails")]
        [Authorize]
        public async Task <IActionResult> UpddateDetails([FromBody] UpdateDetailsModel newDetails)
        {
            var result = await _appUserRepository.UpdatePrivateDetails(newDetails);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("Admin")]
        public async Task <IActionResult> IsUserAdmin(string email)
        {
            if (email == null)
                return BadRequest("user not found");
            var res= await _appUserRepository.IsUserAdmin(email);
            return Ok(res);
        }
    }
}
