using System;

namespace UserApi;

public class Location
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string DoorName { get; set; } // "Main Door", "Office A"
    public required string Room { get; set; } // "Floor 2"

    public ICollection<AccessKey> Keys { get; set; } = new List<AccessKey>();
   
}
