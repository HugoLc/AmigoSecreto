using AmigoSecreto.Domain.Entity;
using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Application.AmigoSecreto.Common;
public record ReadUserResult(
    Guid Id,
    string Name,
    string Phone,
    Guid? GroupId,
    List<Gift> Gifts
);

