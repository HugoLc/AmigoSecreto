using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //mapear o user

        //enviar para o repo
        _userRepository.AddUser();

        //criar result e retorna-lo
    }
}
