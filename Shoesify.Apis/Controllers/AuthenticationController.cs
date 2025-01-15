using Microsoft.AspNetCore.Mvc;
using Shoesify.Apis.Common;
using Shoesify.Entities.Models;
using Shoesify.Services.Requests;
using Shoesify.Services.UserService;

namespace Shoesify.Apis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
  private readonly AuthenticationService _authenticationService;

  public AuthenticationController(AuthenticationService authenticationService)
  {
    _authenticationService = authenticationService;
  }
  
  [HttpPost("login")]
  public async Task<IActionResult> Login(LoginRequest request)
  {
    try
    {
      var result = await _authenticationService.Login(request);
      return Ok(new ApiResponse()
      {
        Message = "Login Successfully",
        Payload = result,
      });
    }
    catch (Exception ex)
    {
      return BadRequest(new ApiResponse()
      {
        Message = ex.Message,
      });
    }
  }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        if (_authenticationService.GetUserByEMail(request.email)!=null)
        {
            return BadRequest("Email address is already registered");
        }
        User u = new User
        {
            Name = request.name,
            Email = request.email,
            Password = request.password,
        };
        _authenticationService.AddUser(u);
        return Ok(u);
    }
}