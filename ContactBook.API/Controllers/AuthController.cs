using ContactBook.Data.Entities;
using ContactBook.DTOs;
using ContactBook.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager; 
        public AuthController(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        //[HttpGet("get-token")]
        //public IActionResult GetToken()
        //{
        //    var token = _authService.GenerateJWT();
        //    return Ok(token);
        //}

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager. FindByEmailAsync (model.Email);
                if (user != null)
                {
                    var result = await _authService.Login(user, model.Password);
                    if( result != null && result.Succeeded)
                    {
                        var userToReturn = new ReturnUserDto
                        {

                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,

                        };
                        var token = _authService.GenerateJWT(user);

                        return Ok(new { user = userToReturn, token = token });

                    }
                    ModelState.AddModelError("Password", "Invalid credentials");

                    return BadRequest(ModelState);
                }
                return BadRequest("Invalid credentials");
            }
            return BadRequest(ModelState);

        }
    
    }
}
