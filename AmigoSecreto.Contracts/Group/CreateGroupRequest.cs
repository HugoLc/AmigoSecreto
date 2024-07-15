namespace AmigoSecreto.Contracts.Group;
public record CreateGroupRequest
(
    DateTime DrawDate,
    DateTime GiftsDate,
    string Local,
    string AdminId
);
