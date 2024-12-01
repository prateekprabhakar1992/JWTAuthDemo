public interface IRefreshTokenStore
{
    void StoreToken(string refreshToken, string username);
    bool ValidateToken(string refreshToken, out string username);
    void RevokeToken(string refreshToken);
}