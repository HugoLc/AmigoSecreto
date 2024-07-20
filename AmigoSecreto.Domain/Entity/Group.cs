using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Domain.Entity;
public class Group
{
    public Guid Id { get; set; }
    public DateTime DrawDate { get; set; }
    public DateTime GiftsDate { get; set; }
    public List<Player> Players { get; set; } = [];
    public string Local { get; set; } = string.Empty;
    public required string AdminId { get; set; }
    public bool AreFriendsDrawn { get; private set; } = false;

    public void DrawFriends()
    {
        if (!IsTodayDrawDate())
        {
            throw new InvalidOperationException("Today is not draw date");
        }
        List<Guid> drawnFriends = [];
        int playersLength = Players.Count;
        var random = new Random();
        for (int i = 0; i < playersLength; i++)
        {
            bool isSameIndex;
            do
            {
                var randomIndex = random.Next(playersLength);
                var friend = Players[randomIndex];
                isSameIndex = i == randomIndex;
                if (isSameIndex && IsLastDrawBlocked(i, playersLength) && !drawnFriends.Contains(friend.Id))
                {
                    i = -1;
                    isSameIndex = false;
                    drawnFriends = [];
                }
                else if (!isSameIndex && !drawnFriends.Contains(friend.Id))
                {
                    drawnFriends.Add(friend.Id);
                    Players[i].AddFriend(friend.Id);
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
        var userFriends = Players.Select(p => new UserFriend()
        {
            User = p,
            Friend = Players.First(f => f.Id == p.Id),
            UserPhone = p.Phone
        });
        return userFriends;
    }
    public List<Player> AddPlayer(Player player)
    {
        Players.Add(player);
        return Players;
    }
    private bool IsLastDrawBlocked(int actualIndex, int listLength) => actualIndex + 1 == listLength;
    private bool IsTodayDrawDate()
    {
        var today = DateTime.Now.Date;
        var drawDay = DrawDate.Date;
        return today >= drawDay;
    }
}
