using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi;

/// <summary>
/// Data Transfer Object (DTO) for login requests.
/// Contains the required information for user authentication.
/// </summary>
/// <param name="Identifier">
/// Username or email used to identify the user.
/// </param>
/// <param name="Password">
/// User password. Required for authentication.
/// </param>
public record LogInDTO
    (
        [Required] string Identifier,
        [Required] string Password
    );
