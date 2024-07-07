using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Domain.Entity;
public class Group
{
    public Guid Id { get; set; }
    public DateTime DrawDate { get; set; }
    public DateTime GiftsDate { get; set; }
    public List<User> Users { get; set; } = [];
    public string Local { get; set; } = string.Empty;
    public required User Admin { get; set; }

    public void DrawFriends()
    {

    }
    public IEnumerable<UserFriend>? GetUserFriendsToSend()
    {
        var userFriends = Users.Select(u => new UserFriend()
        {
            User = u,
            Friend = u.Friend
                ?? throw new NullReferenceException("Usuário não possui amigo sorteado"),
            UserPhone = u.Phone
        });
        return userFriends;
    }
    public List<User> AddUser(User user)
    {
        Users.Add(user);
        return Users;
    }
}
