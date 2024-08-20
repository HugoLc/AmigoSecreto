using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries.Group;
public class ReadGroupByIdQueryHandler : IRequestHandler<ReadGroupByIdQuery, ReadGroupResult>
{
    private readonly IGroupRepository _groupRepository;
    public ReadGroupByIdQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    public async Task<ReadGroupResult> Handle(ReadGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetGroup(request.Id);
        if (group == null)
            return null;
        //TODO alterar result para enviar group completo com players e gifts
        var readGroupResult = new ReadGroupResult(
            group.Id,
            group.DrawDate,
            group.GiftsDate,
            group.Local,
            group.AdminId,
            group.AreFriendsDrawn
        );
        return readGroupResult;
    }
}
