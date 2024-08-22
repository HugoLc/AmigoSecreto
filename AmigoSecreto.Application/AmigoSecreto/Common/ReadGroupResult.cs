using AmigoSecreto.Domain.Entity;

namespace AmigoSecreto.Application.AmigoSecreto.Common;
public record ReadGroupResult(
    Guid Id,
    DateTime DrawDate,
    DateTime GiftsDate,
    string Local,
    Guid AdminId,
    bool AreFriendsDrawn,
    List<Player> Players
);
