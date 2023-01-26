namespace FWClient.Core.Authentication
{
    public interface ITokenProvider
    {
        Task<AuthenticationResult> GetTokenAsync();
    }
}