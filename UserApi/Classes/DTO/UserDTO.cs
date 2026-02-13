using System;

namespace UserApi;

public record class UserDTO
{
    public required string UserName {get; init;} = default!;
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }

}
