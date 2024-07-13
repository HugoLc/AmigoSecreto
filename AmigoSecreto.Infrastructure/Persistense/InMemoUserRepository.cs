using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Infrastructure.Persistense;
public class InMemoUserRepository : IUserRepository
{
    private static readonly List<User> _users = [];
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUser(Guid userId)
    {
        return _users.FirstOrDefault(x => x.Id == userId);
    }

    public List<User> GetUsers()
    {
        return _users;
    }
}
