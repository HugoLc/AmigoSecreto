using AmigoSecreto.Application.AmigoSecreto.Common;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public record DeleteUserCommand(
    string UserId
) : IRequest<DeleteUserResult>;
