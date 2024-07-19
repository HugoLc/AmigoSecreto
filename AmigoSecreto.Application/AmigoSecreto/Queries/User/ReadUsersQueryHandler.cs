using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries;
public class ReadUsersQueryHandler : IRequestHandler<ReadUsersQuery, List<ReadUserResult>>
{
    private readonly IUserRepository _repository;
    public ReadUsersQueryHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<List<ReadUserResult>> Handle(ReadUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _repository.GetUsers();
        var usersResult = users.Select(u => new ReadUserResult(
            Id: u.Id,
            Name: u.Name,
            Phone: u.Phone,
            GroupId: u.GroupId,
            Gifts: u.Gifts
        )).ToList();
        return Task.FromResult(usersResult);
    }
}
