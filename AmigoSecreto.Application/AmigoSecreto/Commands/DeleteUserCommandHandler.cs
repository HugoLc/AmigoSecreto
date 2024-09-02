using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResult>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<DeleteUserResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteUser(Guid.Parse(request.UserId));
        return new DeleteUserResult(Guid.Parse(request.UserId));
    }
}
