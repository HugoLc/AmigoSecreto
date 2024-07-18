using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public class AddPlayerToGroupCommandHandler : IRequestHandler<AddPlayerToGroupCommand, AddPlayerResult>
{
    private readonly IGroupRepository _groupRepository;

    public AddPlayerToGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public Task<AddPlayerResult> Handle(AddPlayerToGroupCommand request, CancellationToken cancellationToken)
    {
        var players = _groupRepository.AddPlayer(request.GroupId, request.Player);
        return Task.FromResult(new AddPlayerResult(request.GroupId, players));
    }
}
