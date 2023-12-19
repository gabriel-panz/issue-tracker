using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IssuesApi.Classes;
using IssuesApi.Classes.Exceptions;
using IssuesApi.Domain.Entities;
using IssuesApi.Domain.Input;
using IssuesApi.Repositories.Interfaces;
using IssuesApi.Services.Interfaces;
using LanguageExt.Common;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net;


namespace IssuesApi.Services;

public class AuthService : IAuthService
{
    public readonly static string Secret = "0f264ce3f6c928abcbf4d8a9a9d2fcd3";
    private readonly IUsersRepository _userRepository;

    public AuthService(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private static string GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var claims = new ClaimsIdentity(new[]{
            new Claim(Utils.ClaimTypes.UserId.ToString(), user.Id.ToString())
            }
        );

        var key = Encoding.ASCII.GetBytes(Secret);

        var descriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = claims,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    public async Task<Result<AuthResponse>> LogIn(LogInDTO dto)
    {
        var users = await _userRepository.List(x => x.Login == dto.Login);
        if (!users.Any()) return new(UserNotFoundException.Create());
        var user = users.First();

        if (BC.BCrypt.Verify(dto.Password, user.Password))
        {
            return new AuthResponse()
            {
                Token = GenerateToken(user),
                UserId = user.Id
            };
        }

        return new(UserNotFoundException.Create());
    }
}