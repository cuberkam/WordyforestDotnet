using System.Security.Claims;

namespace WordyforestDotnet.BusinessLayer.Services.Abstract
{
    public interface IJwtService
    {
        string GenerateAccessToken(string userId, string email);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
