using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;
namespace AmigoSecreto.Tests.Domain.Entity;
public class GroupTests
{
    [Fact]
    public void Test()
    {
        var group = GenerateGroup();
        group.DrawFriends();
        Assert.True(true);
    }

    private Group GenerateGroup()
    {
        var user1 = new User()
        {
            Id = new Guid(),
            Name = "user1",
            Phone = "11111",
            Gifts = [
                new Gift() {
                    Link = "link1",
                    Description = "desc1"
                }
            ],
            Password = "111111"
        };
        var user2 = new User()
        {
            Id = new Guid(),
            Name = "user2",
            Phone = "22222",
            Gifts = [
                new Gift() {
                    Link = "link2",
                    Description = "desc2"
                }
            ],
            Password = "222222"
        };
        var user3 = new User()
        {
            Id = new Guid(),
            Name = "user3",
            Phone = "33333",
            Gifts = [
                new Gift() {
                    Link = "link3",
                    Description = "desc3"
                }
            ],
            Password = "333333"
        };

        var group = new Group()
        {
            Admin = user1,
            DrawDate = DateTime.Now,
            GiftsDate = DateTime.Now,
            Id = new Guid(),
            Local = "asd",
            Users = [user1, user2, user3]
        };
        return group;
    }
}
