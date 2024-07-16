using AmigoSecreto.Application.AmigoSecreto.Common;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries;
public record ReadUsersQuery() : IRequest<List<ReadUserResult>>;
