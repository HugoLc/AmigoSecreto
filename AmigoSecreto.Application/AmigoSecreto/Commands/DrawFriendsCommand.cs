using AmigoSecreto.Application.AmigoSecreto.Common;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public record DrawFriendsCommand(
    string GroupId
) : IRequest<DrawFriendsResult>;
