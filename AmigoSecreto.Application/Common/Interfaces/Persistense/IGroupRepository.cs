
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.Common.Interfaces.Persistense;
public interface IGroupRepository
{
    void AddGroup(Group group);
    Group? GetGroup(Guid id);
    List<Group> GetGroups();

    List<Player> AddPlayer(Guid groupId, Player player);
}
