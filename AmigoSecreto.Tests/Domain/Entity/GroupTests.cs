namespace AmigoSecreto.Tests.Domain.Entity;
public class GroupTests : IClassFixture<GroupFixture>
{
    private readonly GroupFixture _fixture;

    public GroupTests(GroupFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void DrawFriends_ShouldNotDrawWhenTheDateIsNotDrawDate()
    {
        var group = _fixture.Group;
        var correctDrawDate = group.DrawDate;
        var futureDate = DateTime.Now.AddMonths(2);
        group.DrawDate = futureDate;
        Assert.Throws<InvalidOperationException>(() => group.DrawFriends());
        group.DrawDate = correctDrawDate;
    }
    [Fact]
    public void DrawFriends_ShouldNotRepeatFriends()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        var friends = group.Players.Select(u => u.FriendId).ToList();
        Assert.Equal(friends.Count, friends.Distinct().Count());
    }
    [Fact]
    public void DrawFriends_UserShouldNotBeItsOwnFriend()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        var playerOnwFriend = group.Players.Where(u => u.FriendId == u.Id);
        Assert.Empty(playerOnwFriend);
    }
    [Fact]
    public void DrawFriends_AllUsersShouldHaveAFriend()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        var nullFriends = group.Players.Where(u => u.FriendId == null);
        Assert.Empty(nullFriends);
    }
    [Fact]
    public void DrawFriends_AreFriendsDrawnShouldBeTrue()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        Assert.True(group.AreFriendsDrawn);
    }
    [Fact]
    public void GetUserFriendsToSend_ShouldReturnNullIfFriendsArentDrawn()
    {
        var group = _fixture.Group;
        var friends = group.GetUserFriendsToSend();
        Assert.Empty(friends);
    }
    [Fact]
    public void GetUserFriendsToSend_ShouldReturnTheSameQuantityOfElementsAsUsers()
    {
        var group = _fixture.Group;
        var friends = group.GetUserFriendsToSend();
        var players = group.Players;

        Assert.Equal(friends.Count(), players.Count);
    }
}
