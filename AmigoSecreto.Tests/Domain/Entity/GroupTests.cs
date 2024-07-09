namespace AmigoSecreto.Tests.Domain.Entity;
public class GroupTests : IClassFixture<GroupFixture>
{
    private readonly GroupFixture _fixture;

    public GroupTests(GroupFixture fixture)
    {
        _fixture = fixture;
    }
    [Fact]
    public void DrawFriends_ShouldNotRepeatFriends()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        var friends = group.Users.Select(u => u.FriendId).ToList();
        Assert.Equal(friends.Count, friends.Distinct().Count());
    }
    [Fact]
    public void DrawFriends_UserShouldNotBeItsOwnFriend()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        var userOnwFriend = group.Users.Where(u => u.FriendId == u.Id);
        Assert.Empty(userOnwFriend);
    }
    [Fact]
    public void DrawFriends_AllUsersShouldHaveAFriend()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        var nullFriends = group.Users.Where(u => u.FriendId == null);
        Assert.Empty(nullFriends);
    }
    [Fact]
    public void DrawFriends_AreFriendsDrawnShouldBeTrue()
    {
        var group = _fixture.Group;
        group.DrawFriends();
        Assert.True(group.AreFriendsDrawn);
    }
}
