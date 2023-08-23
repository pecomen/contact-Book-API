using ContactBook.Data.Entities;
using ContactBook.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email,
                };
                var identityResult = await _userManager.CreateAsync(user, model.password);
                if (identityResult.Succeeded)
                {
                    var result = await _userManager.CreateAsync(user, "regular");
                    if (!result.Succeeded)
                    {
                        foreach (var err in result.Errors)
                        {
                            ModelState.AddModelError(err.Code, err.Description);
                        }
                        return BadRequest(ModelState);
                    }
                    var userToReturn = new ReturnUserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                    };
                    return Ok(user);
                }

                foreach (var err in identityResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }

            }
            return BadRequest(ModelState);
        }
    }
}
    
        //[HttpGet("get-all")]
        //public IActionResult GetAll()
        //{
        //    var  users = _userManager.Users.ToList();
        //    var usersToReturn = new List<ReturnUserDto>();
        //    if(users.Any())
        //    {
        //        foreach (var user in users)
        //        {
        //            usersToReturn.Add(new ReturnUserDto
        //            {
        //                Id = user.Id,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                PhoneNumber = user.PhoneNumber,
        //                Email = user.Email,

        //            });
        //        }
        //    }
        //    return Ok(usersToReturn);
        //}



        //[HttpGet("single/{id}")]
        //public async Task<IActionResult> GetSingle(string  id)
        //{
        //    var user = await  _userManager.FindByIdAsync(id);
            
        //    if (user != null)
        //    {
        //           var userToReturn = new ReturnUserDto
        //          {
             
        //                Id = user.Id,
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                PhoneNumber = user.PhoneNumber,
        //                Email = user.Email,

        //            };
        //        return Ok(userToReturn);
        //    }
        //    return NotFound($"No user was found with id: {id}");
            
            
        //}


//        [HttpPost("add-user-roles")]
//        public async Task<IActionResult> AddUserRole([FromBody] AddUserRoleDto model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new AppUser
//                {
//                    FirstName = model.FirstName,
//                    LastName = model.LastName,
//                    PhoneNumber = model.PhoneNumber,
//                    UserName = model.Email,
//                };
//                var identityResult = await _userManager.CreateAsync(user, model.Password);
//                if (identityResult.Succeeded)
//                {
//                    var userToReturn = new ReturnUserDto
//                    {
//                        Id = user.Id,
//                        FirstName = user.FirstName,
//                        LastName = user.LastName,
//                        PhoneNumber = user.PhoneNumber,
//                        Email = user.Email,
//                    };
//                    return Ok(userToReturn);
//                }

//                foreach (var err in identityResult.Errors)
//                {
//                    ModelState.AddModelError(err.Code, err.Description);
//                }

//            }

//            if (ModelState.IsValid)
//            {
//                var user = await _userManager.FindByIdAsync(model.UserId);
//                if (user == null)
//                    return NotFound($"Could not find user with Id: {model.UserId}");

//                var result = await _userManager.AddToRoleAsync(user, model.Role);
//                if(!result.Succeeded)
//                {
//                    foreach(var err in result.Errors)
//                    {
//                        ModelState.AddModelError(err.Code, err.Description);
//                    }
//                }
//                return Ok("Role added to user!");


//            }
//            return BadRequest(ModelState);


//        }
     

//        [HttpPost("get-user-roles/{userId")]
//        public async Task<IActionResult> GetUserRoles(string userId)
//        {
//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//                return NotFound($"could not find user with Id: {userId}");

//            var userRoles = await _userManager.GetRolesAsync(user);
//            return Ok(userRoles);
//        }
//    }
//}
