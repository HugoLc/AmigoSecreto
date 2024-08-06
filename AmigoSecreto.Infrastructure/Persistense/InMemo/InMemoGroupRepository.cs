using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Infrastructure.Persistense.InMemo;
public class InMemoGroupRepository : IGroupRepository
{
    private static readonly List<Group> _groups = [];
    public async Task AddGroup(Group group)
    {
        _groups.Add(group);
    }

    public async Task<List<Player>> AddPlayers(Guid groupId, List<Player> players)
    {
        var group = _groups.FirstOrDefault(g => g.Id == groupId) ?? throw new ArgumentException("grupo não encontrado");
        foreach (var player in players)
        {
            group.AddPlayer(player);
        }
        return group.Players;
    }

    public Group DrawDriends(Guid groupId)
    {
        var group = _groups.FirstOrDefault(g => g.Id == groupId) ?? throw new ArgumentException("grupo não encontrado");
        group.DrawFriends();
        return group;
    }

    public Task<Group?> GetGroup(Guid id)
    {
        return Task.FromResult(_groups.FirstOrDefault(g => g.Id == id));
    }

    public List<Group> GetGroups()
    {
        return _groups;
    }

}
