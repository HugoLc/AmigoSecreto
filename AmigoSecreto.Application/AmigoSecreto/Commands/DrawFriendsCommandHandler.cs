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

    public async Task<DrawFriendsResult> Handle(DrawFriendsCommand request, CancellationToken cancellationToken)
    {
        var drawnGroup = await _groupRepository.DrawDriends(Guid.Parse(request.GroupId));
        var result = new DrawFriendsResult(drawnGroup.Id, drawnGroup.AreFriendsDrawn);
        //TODO: enviar mensagens para amigos
        return result;
    }
}
