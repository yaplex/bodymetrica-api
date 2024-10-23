namespace BodyMetrica.Core.Models;

public class Auth0LoginInfo
{
    public string sub { get; set; }
    public string name { get; set; }
    public string picture { get; set; }
    public string email { get; set; }
    public bool email_verified { get; set; }

}