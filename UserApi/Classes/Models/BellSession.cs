using System;

namespace UserApi;

public class BellSession
{
    
    public string BellStatus {get; set;}    
    public string UserId {get; init;}
    public string AcceptedId {get; set;}  

    public DateTime ExpiredAt {get; set;}
    public DateTime CcreatedAt {get; init;} = DateTime.UtcNow;
}
