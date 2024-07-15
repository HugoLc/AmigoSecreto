using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Application.Common.Interfaces.Persistense;
using AmigoSecreto.Domain.Entity;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, CreateGroupResult>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository _userRepository;

    public CreateGroupCommandHandler(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
    }

    public Task<CreateGroupResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = new Group()
        {
            Id = Guid.NewGuid(),
            AdminId = request.AdminId,
            DrawDate = request.DrawDate,
            GiftsDate = request.GiftsDate,
            Local = request.Local,
        };
        var adminUser = _userRepository.GetUser(Guid.Parse(request.AdminId));
        if (adminUser == null)
        {
            //TODO: melhorar tratativa de erros
            throw new ArgumentNullException(nameof(adminUser));
        }
        group.AddUser(adminUser);
        _groupRepository.AddGroup(group);
        var result = new CreateGroupResult(
            group.Id,
            group.DrawDate,
            group.GiftsDate,
            group.Local,
            group.AdminId
        );
        return Task.FromResult(result);
    }
}
