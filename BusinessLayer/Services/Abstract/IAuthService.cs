using WordyforestDotnet.EntityLayer.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace WordyforestDotnet.BusinessLayer.Services.Abstract
{
    public interface IAuthService
    {
        public string? GetCurrentUserId();
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<bool> Logout(string userId);
        Task<IdentityResult> Register(string userEmail, string userPassword);
    }
}
