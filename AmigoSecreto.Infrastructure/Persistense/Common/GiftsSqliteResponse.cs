namespace AmigoSecreto.Infrastructure.Persistense.Common;
public class GiftsSqliteResponse
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required string Description { get; set; }
    public required string? Link { get; set; }

}
