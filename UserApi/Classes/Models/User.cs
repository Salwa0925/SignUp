using System;

namespace UserApi;

/// <summary>
/// User model, represents a user in the system.
/// </summary>
public class User
{

    public Guid Id {get; set;} =Guid.NewGuid();
    public required string UserName {get; set;}
    public required string Email {get; set;}
    public string PasswordHash {get; set;} =string.Empty;
    public bool EmailConfirmed { get; set; } = false;
    public string? EmailConfirmationToken { get; set; }
    
}
