using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Domain.ValueObjects;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public record CreateUserCommand
(
    string Name,
    string Password,
    string Phone,
    List<GiftCommand> Gifts,
    string? GroupId
) : IRequest<CreateUserResult>;

public record GiftCommand(
    string Description,
    string Link
);