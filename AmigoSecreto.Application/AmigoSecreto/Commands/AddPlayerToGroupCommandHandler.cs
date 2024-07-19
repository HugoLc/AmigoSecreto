using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
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
        List<Player> playerObjects = [];
        foreach (var playerDto in request.Players)
        {
            var player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = playerDto.Name,
                Phone = playerDto.Phone,
                Gifts = playerDto.Gifts,
                GroupId = request.GroupId
            };
            playerObjects.Add(player);
        }
        var playersResult = _groupRepository.AddPlayers(request.GroupId, playerObjects);
        return Task.FromResult(new AddPlayerResult(request.GroupId, playersResult));
    }
}
