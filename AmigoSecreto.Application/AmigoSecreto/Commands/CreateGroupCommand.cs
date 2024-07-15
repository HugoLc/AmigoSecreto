using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Domain.Entity;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public record CreateGroupCommand(
    DateTime DrawDate,
    DateTime GiftsDate,
    string Local,
    string AdminId
) : IRequest<CreateGroupResult>;
