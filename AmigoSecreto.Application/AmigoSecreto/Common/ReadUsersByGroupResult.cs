using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Application.AmigoSecreto.Common;
public record ReadUsersByGroupResult(
    Guid Id,
    string Name,
    string Phone,
    Guid? GroupId,
    List<Gift> Gifts
);
