using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Domain.ValueObjects;
public class UserFriend
{
    public required User User { get; set; }
    public required User Friend { get; set; }
    public required string UserPhone { get; set; }
}
