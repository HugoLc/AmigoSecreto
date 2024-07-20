using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Infrastructure.Persistense;
public class InMemoUserRepository : IUserRepository
{
    private static readonly List<User> _users = [];

    public void AddGroup(Guid userId, Guid groupId)
    {
        //TODO: melhorar tratamento de erro
        var user = _users.Find(u=>u.Id == userId) ?? throw new ArgumentException("User not found");
        user.GroupId = groupId;
    }

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
