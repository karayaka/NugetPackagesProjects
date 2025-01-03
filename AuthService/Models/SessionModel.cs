namespace AuthService.Models;

public class SessionModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public string? Image { get; set; }
    public string Token { get; set; }
}