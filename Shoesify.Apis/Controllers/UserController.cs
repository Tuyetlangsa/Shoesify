using Microsoft.AspNetCore.Mvc;
using Shoesify.Services.UserService;
using Shoesify.Services.Requests;
using Shoesify.Entities.Models;

namespace Shoesify.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest userRequest)
        {
            if (string.IsNullOrEmpty(userRequest.userId))
            {
                return BadRequest("UserId cannot be null or empty.");
            }

            var userId = _userService.GetUserById(userRequest.userId);
            if (userId == null)
            {
                return NotFound("User not found.");
            }

            var existingUser = _userService.GetUserById(userRequest.userId);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            existingUser.Name = userRequest.Name;
            existingUser.Email = userRequest.Email;
            existingUser.Phone = userRequest.Phone;
            existingUser.DateOfBirth = userRequest.DateOfBirth;
            existingUser.Password = userRequest.Password;
            existingUser.Address = userRequest.Address;
            return Ok(_userService.updateUser(existingUser));
        }
        [HttpPut("UpdateStatus")]
        public IActionResult UpdateUserStatus(String UserId, bool status)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return BadRequest("UserId cannot be null or empty.");
            }

            var userId = _userService.GetUserById(UserId);
            if (userId == null)
            {
                return NotFound("User not found.");
            }
            var user = new User
            {
                UserId = UserId,
                Status = status
            };
            return Ok(_userService.updateUser(user));
        }
    }
}
