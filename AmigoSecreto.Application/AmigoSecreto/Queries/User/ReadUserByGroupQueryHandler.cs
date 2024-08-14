using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries.User;
public class ReadUserByGroupQueryHandler : IRequestHandler<ReadUsersByGroupQuery, List<ReadUsersByGroupResult>>
{
    private readonly IUserRepository _userRepository;
    public ReadUserByGroupQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<ReadUsersByGroupResult>> Handle(ReadUsersByGroupQuery request, CancellationToken cancellationToken)
    {
        var players = await _userRepository.GetPlayersByGroup(request.Id);

        if (players == null || players.Count == 0)
        {
            return null;
        }
        var playersResponse = players.Select(p => new ReadUsersByGroupResult(
            p.Id,
            p.Name,
            p.Phone,
            p.GroupId,
            p.Gifts
        )).ToList();
        return playersResponse;
    }
}
