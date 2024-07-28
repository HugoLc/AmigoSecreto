using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public class AddPlayerToGroupCommandHandler : IRequestHandler<AddPlayerToGroupCommand, AddPlayerResult>
{
    private readonly IGroupRepository _groupRepository;

    public AddPlayerToGroupCommandHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<AddPlayerResult> Handle(AddPlayerToGroupCommand request, CancellationToken cancellationToken)
    {
        List<Player> playerObjects = [];
        foreach (var playerDto in request.Players)
        {
            var playerId = Guid.NewGuid();
            var gifts = playerDto.Gifts?.Select(g => new Gift()
            {
                Id = Guid.NewGuid(),
                UserId = playerId,
                Description = g.Description,
                Link = g.Link
            }).ToList();
            var player = new Player()
            {
                Id = playerId,
                Name = playerDto.Name,
                Phone = playerDto.Phone,
                Gifts = gifts,
                GroupId = request.GroupId
            };
            playerObjects.Add(player);
        }
        var playersResult = await _groupRepository.AddPlayers(request.GroupId, playerObjects);
        return new AddPlayerResult(request.GroupId, playersResult);
    }
}
