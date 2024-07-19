
namespace AmigoSecreto.Contracts.Group;
public record AddPlayersRequest(
    string GroupId,
    List<PlayerDTO> Players
);
