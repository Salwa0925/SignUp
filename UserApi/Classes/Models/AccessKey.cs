using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UserApi;

public record AccessKey
{
    public Guid Id {get; init;} = Guid.NewGuid();

    public required Guid UserId {get; init;}
    public User User {get; init;} = null!;
     public Guid LocationId {get; init;}
    public Location Location {get; init;} = null!;

    public bool IsActive {get; set;} = true;
    public DateTime CcreatedAt {get; init;} = DateTime.UtcNow;
    public DateTime ExpiersAt {get; set;}

}
