using Microsoft.AspNetCore.Mvc;
using Shoesify.Apis.Common;
using Shoesify.Services;
using Shoesify.Services.Requests;

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
}