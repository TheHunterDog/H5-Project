namespace Database.Model;

public interface IAuthenticatable
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    
}