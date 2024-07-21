using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.Common.Interfaces.Persistense;
public interface IUserRepository
{
    void AddGroup(Guid userId, Guid groupId);
    Task AddUser(User user);
    User? GetUser(Guid userId);
    List<User> GetUsers();
}
