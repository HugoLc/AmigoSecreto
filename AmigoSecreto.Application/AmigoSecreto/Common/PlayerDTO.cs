using AmigoSecreto.Application.AmigoSecreto.Common;
using AmigoSecreto.Domain.ValueObjects;

namespace AmigoSecreto.Contracts.Group;
public record PlayerDTO(
    string Name,
    string Phone,
    List<GiftDTO>? Gifts
);
