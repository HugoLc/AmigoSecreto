using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Domain.ValueObjects;
public class UserFriend
{
    public required Player User { get; set; }
    public required Player Friend { get; set; }
    public required string UserPhone { get; set; }
}
