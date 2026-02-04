using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace UserApi;

/// <summary>
///  Implementation of IUserService (interface) for managing user sign-up and login.
///  Provides methods to create users and authenticate them.
///  checks for existing emails and usernames to prevent duplicates.
/// </summary>
public class UserService(AppDbContext context) : IUserService
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
            Email = dto.Email
            
        };
        user.PasswordHash = _hasher.HashPassword(user, dto.Password);
        
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();


        return ServiceResult<string>.CreateResult("User created");
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

        return ServiceResult<string>.CreateResult("Welcome back");
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
}

