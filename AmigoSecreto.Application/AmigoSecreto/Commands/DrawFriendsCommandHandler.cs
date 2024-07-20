using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public class DrawFriendsCommandHandler : IRequestHandler<DrawFriendsCommand, DrawFriendsResult>
{
    private readonly IGroupRepository _groupRepository;
    public DrawFriendsCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public Task<DrawFriendsResult> Handle(DrawFriendsCommand request, CancellationToken cancellationToken)
    {
        var drawnGroup = _groupRepository.DrawDriends(Guid.Parse(request.GroupId));
        var result = new DrawFriendsResult(drawnGroup.Id, drawnGroup.AreFriendsDrawn);
        return Task.FromResult(result);
    }
}
