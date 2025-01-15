using Microsoft.EntityFrameworkCore;
using Shoesify.Entities.Models;
using Shoesify.Repository.Users;
using Shoesify.Services.Requests;

namespace Shoesify.Services.UserService;

public class AuthenticationService
{
    private readonly ShoesifyContext _context;
    private readonly JwtTokenService _jwtTokenService;
    private readonly IUserRepository _userRepository;
    private readonly IDictionary<string, string> _credentials = new Dictionary<string, string>();

    public AuthenticationService(ShoesifyContext context, JwtTokenService jwtTokenService, IUserRepository userRepository)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
        _userRepository = userRepository;
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
        //return _jwtTokenService.GenerateJwtToken(user);
        var token = _jwtTokenService.GenerateJwtToken(user);
        _credentials[token] = user.Email;
        return token;
    }

    public void Logout(string token)
    {
        _credentials.Remove(token);
    }

    public User GetUserByEMail(string Email)
    {
        return _userRepository.GetByEmail(Email);
    }

    public User AddUser(User user)
    {
        return _userRepository.Add(user);
    }

}