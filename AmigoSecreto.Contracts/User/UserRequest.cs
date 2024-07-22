
namespace AmigoSecreto.Contracts.User;
public record UserRequest
{
    public required string Name { get; set; } = string.Empty;
    public required string Password { get; set; } = string.Empty;
    public required string Phone { get; set; } = string.Empty;
    public List<GiftRequest> Gifts { get; set; } = [];
    public string? GroupId { get; set; } = null;
}
