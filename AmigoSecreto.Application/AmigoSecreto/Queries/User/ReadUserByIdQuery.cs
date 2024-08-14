using AmigoSecreto.Application.AmigoSecreto.Common;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries;
public record ReadUserByIdQuery(
    Guid Id
) : IRequest<ReadUserResult>;
