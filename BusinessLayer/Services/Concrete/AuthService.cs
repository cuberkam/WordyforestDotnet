using EntityLayer.Entities;
using WordyforestDotnet.EntityLayer.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace WordyforestDotnet.BusinessLayer.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<ExtendedUser> userManager, IConfiguration configuration, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public string? GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new LoginResponseDto {IsSuccess = false, Message = "Invalid email or password" };

            var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Email!);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _userManager.SetAuthenticationTokenAsync(user, "RefreshTokenProvider", "RefreshToken", refreshToken);

            return new LoginResponseDto
            {
                IsSuccess = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = Convert.ToInt32(_configuration["Jwt:AccessTokenExpirationInMinutes"]) * 60,
                User = new UserDto { Id = user.Id, Email = user.Email! }
            };
        }

        public async Task<bool> Logout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            await _userManager.RemoveAuthenticationTokenAsync(user, "RefreshTokenProvider", "RefreshToken");
            return true;
        }

        public async Task<IdentityResult> Register(string userEmail, string userPassword)
        {
            var user = new ExtendedUser
            {
                UserName = userEmail,
                Email = userEmail
            };

            return await _userManager.CreateAsync(user, userPassword);
        }
    }
}
