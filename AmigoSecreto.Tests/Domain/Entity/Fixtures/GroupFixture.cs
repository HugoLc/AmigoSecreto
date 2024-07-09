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
        var users = GenerateUsers(6);
        var group = new Group()
        {
            Admin = users[0],
            DrawDate = DateTime.Now,
            GiftsDate = DateTime.Now,
            Id = Guid.NewGuid(),
            Local = "Local",
            Users = users
        };
        return group;
    }

    private List<User> GenerateUsers(int count)
    {
        var users = new List<User>();
        for (int i = 1; i <= count; i++)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = $"user{i}",
                Phone = $"{i}",
                Gifts = new List<Gift>
                {
                    new Gift()
                    {
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
