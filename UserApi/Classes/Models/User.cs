using System;

namespace UserApi;

/// <summary>
/// User model, represents a user in the system.
/// </summary>
public class User
{

    public Guid Id {get; init;} = Guid.NewGuid();
    public required string UserName {get; set;}
    public required string Email {get; set;}
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public UserRole Role {get; set;} = UserRole.User;
    public string PasswordHash {get; set;} =string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
}
