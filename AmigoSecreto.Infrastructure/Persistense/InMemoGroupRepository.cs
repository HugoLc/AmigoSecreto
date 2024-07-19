using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Infrastructure.Persistense;
public class InMemoGroupRepository : IGroupRepository
{
    private static readonly List<Group> _groups = [];
    public void AddGroup(Group group)
    {
        _groups.Add(group);
    }

    public List<Player> AddPlayers(Guid groupId, List<Player> players)
    {
        var group = _groups.FirstOrDefault(g => g.Id == groupId) ?? throw new ArgumentException("grupo não encontrado");
        foreach (var player in players)
        {
            group.AddPlayer(player);
        }
        return group.Players;
    }

    public Group? GetGroup(Guid id)
    {
        return _groups.FirstOrDefault(g => g.Id == id);
    }

    public List<Group> GetGroups()
    {
        return _groups;
    }
}
