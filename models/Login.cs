// LoginModel class for binding the request body
public record LoginModel
{
    public string Username { get; init; }
    public string Password { get; init; }
}