namespace UserApi.Enums;

public enum BellStatus
{
    Ringing,
    Accepted,
    Wait,
    Expired,     // 3 min passed
    Cancelled
}
