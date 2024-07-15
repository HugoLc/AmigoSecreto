using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Domain.Entity;
public class Group
{
    public Guid Id { get; set; }
    public DateTime DrawDate { get; set; }
    public DateTime GiftsDate { get; set; }
    public List<User> Users { get; set; } = [];
    public string Local { get; set; } = string.Empty;
    public required string AdminId { get; set; }
    public bool AreFriendsDrawn { get; private set; } = false;

    public void DrawFriends()
    {
        List<Guid> drawnFriends = [];
        int usersLength = Users.Count;
        var random = new Random();
        for (int i = 0; i < usersLength; i++)
        {
            bool isSameIndex;
            do
            {
                var randomIndex = random.Next(usersLength);
                var friend = Users[randomIndex];
                isSameIndex = i == randomIndex;
                if (isSameIndex && IsLastDrawBlocked(i, usersLength) && !drawnFriends.Contains(friend.Id))
                {
                    i = -1;
                    isSameIndex = false;
                    drawnFriends = [];
                }
                else if (!isSameIndex && !drawnFriends.Contains(friend.Id))
                {
                    drawnFriends.Add(friend.Id);
                    Users[i].AddFriend(friend.Id);
                }
                else
                {
                    isSameIndex = true;
                }
            } while (isSameIndex);
        }
        AreFriendsDrawn = true;
    }
    public IEnumerable<UserFriend> GetUserFriendsToSend()
    {
        if (!AreFriendsDrawn)
        {
            return [];
        }
        var userFriends = Users.Select(u => new UserFriend()
        {
            User = u,
            Friend = Users.First(f => f.Id == u.Id),
            UserPhone = u.Phone
        });
        return userFriends;
    }
    public List<User> AddUser(User user)
    {
        Users.Add(user);
        return Users;
    }
    private bool IsLastDrawBlocked(int actualIndex, int listLength) => actualIndex + 1 == listLength;
}
