namespace UserMicroService.Domain;

public class UserInfo
{
    public int Id { get; set; }
    public string Uuid { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
