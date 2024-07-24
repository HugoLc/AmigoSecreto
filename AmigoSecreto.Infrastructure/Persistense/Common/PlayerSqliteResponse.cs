namespace AmigoSecreto.Infrastructure.Persistense.Common;
public class PlayerSqliteResponse
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public string GroupId { get; set; } = string.Empty;
}
