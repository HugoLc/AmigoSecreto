using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Contracts.Group;
public record AddPlayersRequest(
    string GroupId,
    List<Player> Players
);
