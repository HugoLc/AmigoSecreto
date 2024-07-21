using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Password = request.Password,
            Phone = request.Phone,
            Gifts = request.Gifts,
        };
        await _userRepository.AddUser(user);
        return new CreateUserResult(Id: user.Id, Name: user.Name);
    }
}
