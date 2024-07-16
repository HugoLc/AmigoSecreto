using System.Text.RegularExpressions;
using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Domain.ValueObjects;
using MediatR;

namespace AmigoSecreto.Application.AmigoSecreto.Queries;
public record ReadUserByIdQuery(
    Guid Id
) : IRequest<ReadUserResult>;
