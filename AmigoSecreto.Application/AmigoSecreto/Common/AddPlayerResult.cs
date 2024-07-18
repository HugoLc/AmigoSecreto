
using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.AmigoSecreto.Common;
public record AddPlayerResult(
    Guid GroupId,
    List<Player> PlayerList
);
