using Microsoft.EntityFrameworkCore;
using Shoesify.Entities.Models;
using Shoesify.Services.Requests;

namespace Shoesify.Services;

public class AuthenticationService
{
    private readonly ShoesifyContext _context;
    private readonly JwtTokenService _jwtTokenService;

    public AuthenticationService(ShoesifyContext context, JwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }
    
    public async Task<string?> Login(LoginRequest request)
    {
        User? user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email.Equals(request.Email));

        // Check if the user does not exist or if the password is incorrect
        if (user == null || !user.Password.Equals(request.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password");
        }

        // Check if the user's account is disabled
        if ((bool)(!user.Status)!)
        {
            throw new UnauthorizedAccessException("User account is disabled");
        }

        // Generate and return the JWT token if user is found and account is active
        return _jwtTokenService.GenerateJwtToken(user);
    } 
    
    
}