using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.Common.Interfaces.Persistense;
public interface IUserRepository
{
    Task AddGroup(Guid userId, Guid groupId);
    Task AddUser(User user);
    Task<Player?> GetPlayer(Guid userId);
    List<Player> GetPlayers();
}
