using AmigoSecreto.Application.AmigoSecreto.Common;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries.Group;
public record ReadGroupsQuery() : IRequest<List<ReadGroupResult>>;
