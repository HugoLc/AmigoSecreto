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

    public async Task<CreateGroupResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        DateTime convertedDrawDate = DateTime.Parse(request.DrawDate);
        DateTime convertedGiftsDate = DateTime.Parse(request.GiftsDate);
        if (!AreValidDates(convertedDrawDate, convertedGiftsDate))
        {
            //TODO: tratar erros
            throw new ArgumentException("As datas precisam ser futuras e a data do sorteio precisa ser anterior a data de troca de presentes");
        }
        var group = new Group()
        {
            Id = Guid.NewGuid(),
            AdminId = request.AdminId,
            DrawDate = convertedDrawDate,
            GiftsDate = convertedGiftsDate,
            Local = request.Local,
        };
        var adminUser = await _userRepository.GetUser(Guid.Parse(request.AdminId));
        _userRepository.AddGroup(Guid.Parse(request.AdminId), group.Id);
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
        return result;
    }

    private static bool AreValidDates(DateTime convertedDrawDate, DateTime convertedGiftsDate)
    {
        DateTime today = DateTime.Today;
        return convertedGiftsDate < convertedDrawDate || today >= convertedDrawDate.Date || today >= convertedGiftsDate;
    }
}
