using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.Common.Interfaces.Persistense;
public interface IUserRepository
{
    void AddUser(User user);
    User? GetUser(Guid userId);
}
