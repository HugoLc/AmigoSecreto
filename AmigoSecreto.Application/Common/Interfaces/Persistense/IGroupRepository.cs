
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.Common.Interfaces.Persistense;
public interface IGroupRepository
{
    Task AddGroup(Group group);
    Group? GetGroup(Guid id);
    List<Group> GetGroups();
    List<Player> AddPlayers(Guid groupId, List<Player> players);
    Group DrawDriends(Guid groupId);
}
