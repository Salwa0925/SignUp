using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi;


/// <summary>
/// Data Transfer Object (DTO) for user registration.
/// Contains required fields and validation rules for creating a new user.
/// </summary>
public record class SignUpDTO 
{
    [Required] 
    [StringLength(3-15)]
    public required string UserName {get; init => field = value.ToLowerInvariant();} 

    [Required]
    [EmailAddress] 
    public required string Email {get; init => field = value.ToLowerInvariant();}
    
    [Required]
    [MinLength(10)]
    [MaxLength(50)]
    // [RegularExpression] attribute to enforce password complexity rules.
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{10,50}$",
    ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
    public required string Password {get; init;} 

    [Required]
    [MaxLength(20)]
    [MinLength(3)]
    public required string FirstName {get; init;}
    
    [Required]
    [MaxLength(20)]
    [MinLength(3)]
    public required string LastName {get; init;}
   

}
