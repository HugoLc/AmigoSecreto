using AmigoSecreto.Application.AmigoSecreto.Common;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries.User;
public record ReadUsersByGroupQuery(
    Guid Id
) : IRequest<List<ReadUsersByGroupResult>>;
