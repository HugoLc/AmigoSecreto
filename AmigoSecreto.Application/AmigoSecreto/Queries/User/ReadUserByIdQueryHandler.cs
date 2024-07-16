using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries;
public class ReadUserByIdQueryHandler : IRequestHandler<ReadUserByIdQuery, ReadUserResult>
{
    private readonly IUserRepository _userRepository;
    public ReadUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ReadUserResult> Handle(ReadUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUser(request.Id);
        if (user == null)
        {
            return null;
        }
        var readUserResult = new ReadUserResult(
            user.Id,
            user.Name,
            user.Phone,
            user.Group?.Id,
            user.Gifts
        );
        return readUserResult;
    }
}
