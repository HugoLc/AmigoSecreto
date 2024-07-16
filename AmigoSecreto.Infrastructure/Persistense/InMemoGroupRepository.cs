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

    public Group? GetGroup(Guid id)
    {
        return _groups.FirstOrDefault(g => g.Id == id);
    }
}
