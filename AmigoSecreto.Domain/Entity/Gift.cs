namespace AmigoSecreto.Domain.ValueObjects;
public class Gift
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required string Description { get; set; } = string.Empty;
    public string? Link { get; set; } = string.Empty;
}
