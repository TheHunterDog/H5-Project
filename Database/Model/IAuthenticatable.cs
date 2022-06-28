namespace Database.Model;

public interface IAuthenticatable
{
    public string  AUTHUUID { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    
    public IEnumerable<Notification> Notifications { get; set; }
    
}