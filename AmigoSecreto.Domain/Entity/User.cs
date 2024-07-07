using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Domain.Entity;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public User? Friend { get; set; } = null;
    public List<Gift> Gifts { get; set; } = [];
    public Group? Group { get; set; } = null;

    public void AddGift(Gift gift)
    {
        Gifts.Add(gift);
    }
    public void RemoveGift(Gift gift)
    {
        Gifts.Remove(gift);
    }
    public void AddFriend(User friend)
    {
        Friend = friend;
    }
}
