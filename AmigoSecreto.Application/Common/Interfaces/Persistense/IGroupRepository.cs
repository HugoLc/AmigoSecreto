
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.Common.Interfaces.Persistense;
public interface IGroupRepository
{
    Task AddGroup(Group group);
    Task<Group?> GetGroup(Guid id);
    List<Group> GetGroups();
    Task<List<Player>> AddPlayers(Guid groupId, List<Player> players);
    Task<Group> DrawDriends(Guid groupId);
    Task UpdateGroup(Group group);
}
