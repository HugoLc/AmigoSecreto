using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Contracts.Group;
using AmigoSecreto.Domain.Entity;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public record AddPlayerToGroupCommand(
    Guid GroupId,
    List<PlayerDTO> Players
) : IRequest<AddPlayerResult>;
