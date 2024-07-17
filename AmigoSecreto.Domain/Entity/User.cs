
namespace AmigoSecreto.Domain.Entity;
public class User : Player
{
    public required string Password { get; set; } = string.Empty;
}
