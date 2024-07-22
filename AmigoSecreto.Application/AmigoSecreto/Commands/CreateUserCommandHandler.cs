using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;
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
        var userId = Guid.NewGuid();
        var user = new User()
        {
            Id = userId,
            Name = request.Name,
            Password = request.Password,
            Phone = request.Phone,
            Gifts = request.Gifts.Select(g => new Gift()
            {
                Id = Guid.NewGuid(),
                Description = g.Description,
                Link = g.Link,
                UserId = userId
            }).ToList(),
        };
        await _userRepository.AddUser(user);
        return new CreateUserResult(Id: user.Id, Name: user.Name);
    }
}
