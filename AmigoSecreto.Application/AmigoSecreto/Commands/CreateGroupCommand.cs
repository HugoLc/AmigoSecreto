using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Domain.Entity;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Commands;
public record CreateGroupCommand(
    string DrawDate,
    string GiftsDate,
    string Local,
    string AdminId
) : IRequest<CreateGroupResult>;
