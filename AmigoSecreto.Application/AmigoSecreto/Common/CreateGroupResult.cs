
namespace AmigoSecreto.Application.AmigoSecreto.Common;
public record CreateGroupResult(
    Guid Id,
    DateTime DrawDate,
    DateTime GiftsDate,
    string Local,
    Guid AdminId
);
