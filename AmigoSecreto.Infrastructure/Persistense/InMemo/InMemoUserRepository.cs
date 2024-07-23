using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Infrastructure.Persistense.InMemo;
public class InMemoUserRepository : IUserRepository
{
    private static readonly List<User> _users = [];

    public void AddGroup(Guid userId, Guid groupId)
    {
        //TODO: melhorar tratamento de erro
        var user = _users.Find(u => u.Id == userId) ?? throw new ArgumentException("User not found");
        user.GroupId = groupId;
    }

    public async Task AddUser(User user)
    {
        _users.Add(user);
    }

    public Task<Player?> GetPlayer(Guid userId)
    {
        // return Task.FromResult(_users.FirstOrDefault(x => x.Id == userId));
        throw new NotImplementedException();
    }

    public List<Player> GetPlayers()
    {
        // return _users;
        throw new NotImplementedException();
    }
}
