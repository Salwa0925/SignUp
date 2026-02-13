using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace UserApi;

/// <summary>
///  Implementation of IUserService (interface) for managing user sign-up and login.
///  Provides methods to create users and authenticate them.
///  checks for existing emails and usernames to prevent duplicates.
/// </summary>
public class UserService(AppDbContext context, IConfiguration configuration, IEmailService emailService) : IUserService

{
    private readonly PasswordHasher<User> _hasher = new();
    public async Task<ServiceResult<string>> CreateUser(SignUpDTO dto)
    {
        if (await context.Users.AnyAsync(u => u.Email == dto.Email))
            return ServiceResult<string>.CreateFailure(message: "Email already exists");
   
        if (await context.Users.AnyAsync(u=> u.UserName == dto.UserName))
            return ServiceResult<string>.CreateFailure(message: "UserName already exists");

        var user = new User
        {
            UserName =dto.UserName,
            Email = dto.Email,
            EmailConfirmationToken = Guid.NewGuid().ToString()
            
        };
        user.PasswordHash = _hasher.HashPassword(user, dto.Password);
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        await emailService.SendConfirmationEmail(user.Email, user.EmailConfirmationToken);


        return ServiceResult<string>.CreateResult("User created. Please check your email to confirm your account.");
    }

    public async Task<ServiceResult<string>> Login(LogInDTO dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u =>
            u.Email == dto.Identifier ||
            u.UserName == dto.Identifier);

        if (user is null)
            return ServiceResult<string>.CreateFailure("Invalid credentials");

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (result == PasswordVerificationResult.Failed)
            return ServiceResult<string>.CreateFailure("Invalid credentials");

        if (!user.EmailConfirmed)
            return ServiceResult<string>.CreateFailure("Please confirm your email before logging in");


        var token = GenerateJwtToken(user);
        return ServiceResult<string>.CreateResult(token);
    }

    public async Task<ServiceResult<IEnumerable<UserDTO>>> GetAllUsers()
    {
        var userDTOsList = await context.Users.Select(u => new UserDTO
        {
            Email = u.Email,
            UserName = u.UserName
        }).ToListAsync();

        return ServiceResult<IEnumerable<UserDTO>>.CreateResult(userDTOsList);

    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var expireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"] ?? "60");

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
        public async Task<ServiceResult<string>> ConfirmEmail(string token)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);

        if (user is null)
            return ServiceResult<string>.CreateFailure("Invalid confirmation token");

        user.EmailConfirmed = true;
        user.EmailConfirmationToken = null;
        await context.SaveChangesAsync();

        return ServiceResult<string>.CreateResult("Email confirmed successfully");
    }

}

