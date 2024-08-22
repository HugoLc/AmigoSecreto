using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries.Group;
public class ReadGroupsQueryHandler : IRequestHandler<ReadGroupsQuery, List<ReadGroupResult>>
{
    private readonly IGroupRepository _groupRepository;

    public ReadGroupsQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public Task<List<ReadGroupResult>> Handle(ReadGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = _groupRepository.GetGroups();
        var resultGroups = groups.Select(g => new ReadGroupResult(
            g.Id,
            g.DrawDate,
            g.GiftsDate,
            g.Local,
            g.AdminId,
            g.AreFriendsDrawn,
            g.Players
        )).ToList();
        return Task.FromResult(resultGroups);
    }
}
