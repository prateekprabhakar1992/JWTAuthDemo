public class InMemoryRefreshTokenStore : IRefreshTokenStore
{
    private readonly Dictionary<string, string> _refreshTokens = new();

    public void StoreToken(string refreshToken, string username) => _refreshTokens[refreshToken] = username;

    public bool ValidateToken(string refreshToken, out string username)
    {
        return _refreshTokens.TryGetValue(refreshToken, out username);
    }

    public void RevokeToken(string refreshToken) => _refreshTokens.Remove(refreshToken);
}