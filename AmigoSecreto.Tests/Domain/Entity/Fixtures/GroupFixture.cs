using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;

public class GroupFixture
{
    public Group Group { get; private set; }

    public GroupFixture()
    {
        Group = GenerateGroup();
    }

    private Group GenerateGroup()
    {
        var players = GeneratePlayers(6);
        var group = new Group()
        {
            AdminId = players[0].Id.ToString(),
            DrawDate = DateTime.Now,
            GiftsDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Local = "Local",
            Players = players
        };
        return group;
    }

    private List<Player> GeneratePlayers(int count)
    {
        var users = new List<Player>();
        for (int i = 1; i <= count; i++)
        {
            var userId = Guid.NewGuid();
            var user = new User()
            {
                Id = userId,
                Name = $"user{i}",
                Phone = $"{i}",
                Gifts = new List<Gift>
                {
                    new Gift()
                    {
                        Id =Guid.NewGuid(),
                        UserId = userId,
                        Link = $"link{i}",
                        Description = $"desc{i}"
                    }
                },
                Password = $"{i}{i}{i}{i}{i}{i}"
            };
            users.Add(user);
        }
        return users;
    }
}
