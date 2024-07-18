using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Domain.Entity;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public record AddPlayerToGroupCommand(
    Guid GroupId,
    Player Player //TODO: mudar para algo mais generio sem ID. O Id deve ser adicionado no handler
) : IRequest<AddPlayerResult>;
