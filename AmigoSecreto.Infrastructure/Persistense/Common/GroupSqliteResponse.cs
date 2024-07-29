namespace AmigoSecreto.Infrastructure.Persistense.Common;
public class GroupSqliteResponse
{
    public string Id { get; set; } = string.Empty;
    public string DrawDate { get; set; } = string.Empty;
    public string GiftsDate { get; set; } = string.Empty;
    public string Local { get; set; } = string.Empty;
    public string AdminId { get; set; } = string.Empty;
    public bool AreFriendsDrawn { get; private set; } = false;
}
