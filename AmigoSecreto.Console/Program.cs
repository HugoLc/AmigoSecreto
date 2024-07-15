// See https://aka.ms/new-console-template for more information
using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var group = GenerateGroup();
group.DrawFriends();
var json = JsonConvert.SerializeObject(group.Users, Formatting.Indented);
Console.WriteLine(json);

Group GenerateGroup()
{
    var user1 = new User()
    {
        Id = Guid.NewGuid(),
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
        Id = Guid.NewGuid(),
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
        Id = Guid.NewGuid(),
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
    var user4 = new User()
    {
        Id = Guid.NewGuid(),
        Name = "user4",
        Phone = "4",
        Gifts = [
            new Gift() {
                    Link = "link4",
                    Description = "desc4"
                }
        ],
        Password = "4"
    };
    var user5 = new User()
    {
        Id = Guid.NewGuid(),
        Name = "user5",
        Phone = "5",
        Gifts = [
            new Gift() {
                    Link = "link5",
                    Description = "desc5"
                }
        ],
        Password = "5"
    };
    var user6 = new User()
    {
        Id = Guid.NewGuid(),
        Name = "user6",
        Phone = "6",
        Gifts = [
            new Gift() {
                    Link = "link6",
                    Description = "desc6"
                }
        ],
        Password = "6"
    };

    var group = new Group()
    {
        AdminId = user1.Id.ToString(),
        DrawDate = DateTime.Now,
        GiftsDate = DateTime.Now,
        Id = Guid.NewGuid(),
        Local = "asd",
        Users = [user1, user2, user3, user4, user5, user6]
    };
    return group;
}