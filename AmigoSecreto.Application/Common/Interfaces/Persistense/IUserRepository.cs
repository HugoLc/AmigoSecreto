using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Application.Common.Interfaces.Persistense;
public interface IUserRepository
{
    Task AddGroup(Guid userId, Guid groupId);
    Task AddUser(User user);
    Task<Player?> GetPlayer(Guid userId);
    List<Player> GetPlayers();
    Task<List<Player>> GetPlayersByGroup(Guid groupId);
    Task UpdatePlayer(Player player);
    Task UpadateGifts(Player player);

}
