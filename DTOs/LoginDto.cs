namespace backend_api.DTOs;

public class LoginDto
{
    public string Email { get; set; }

    public string Password { get; set; }

    public LoginDto()
    {
        Email = string.Empty;
        Password = string.Empty;
    }
}