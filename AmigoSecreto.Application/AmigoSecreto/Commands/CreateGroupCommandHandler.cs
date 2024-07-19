using System.Globalization;
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
        DateTime convertedDrawDate = DateTime.Parse(request.DrawDate);
        DateTime convertedGiftsDate = DateTime.Parse(request.GiftsDate);
        //TODO: verificar tambem se é maior que a data atual
        if (convertedGiftsDate < convertedDrawDate)
        {
            //TODO: tratar erros
            throw new ArgumentException("Data do sorteio precisa ser anterior a data de troca de presentes");
        }
        var group = new Group()
        {
            Id = Guid.NewGuid(),
            AdminId = request.AdminId,
            DrawDate = convertedDrawDate,
            GiftsDate = convertedGiftsDate,
            Local = request.Local,
        };
        var adminUser = _userRepository.GetUser(Guid.Parse(request.AdminId));
        if (adminUser == null)
        {
            //TODO: melhorar tratativa de erros
            throw new ArgumentNullException(nameof(adminUser));
        }
        group.AddPlayer(adminUser);
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
