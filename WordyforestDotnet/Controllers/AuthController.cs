using WordyforestDotnet.EntityLayer.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using WordyforestDotnet.BusinessLayer.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using EntityLayer.Entities;

namespace WordyforestDotnet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        private readonly UserManager<ExtendedUser> _userManager;

        public AuthController(IAuthService authService, IJwtService jwtService, UserManager<ExtendedUser> userManager) 
        {
            _authService = authService;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var loginResponse = await _authService.LoginAsync(request);

            if (!loginResponse.IsSuccess) return BadRequest(loginResponse.Message);

            Response.Cookies.Append("refreshToken", loginResponse.RefreshToken!, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict
            });


            return Ok(new { loginResponse.AccessToken, loginResponse.ExpiresIn, loginResponse.User});
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenDto request)
        {
            try
            {
                var principal = _jwtService.GetPrincipalFromExpiredToken(request.AccessToken);
                var userId = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (userId == null)
                {
                    return Unauthorized(new { message = "Invalid token" });
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Unauthorized(new { message = "User not found" });
                }

                var storedRefreshToken = await _userManager.GetAuthenticationTokenAsync(user, "RefreshTokenProvider", "RefreshToken");
                if (storedRefreshToken != request.RefreshToken)
                {
                    return Unauthorized(new { message = "Invalid refresh token" });
                }

                var newAccessToken = _jwtService.GenerateAccessToken(user.Id, user.Email!);
                var newRefreshToken = _jwtService.GenerateRefreshToken();

                await _userManager.SetAuthenticationTokenAsync(user, "RefreshTokenProvider", "RefreshToken", newRefreshToken);

                return Ok(new RefreshTokenDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var result = await _authService.Logout(userId!);
            if (!result) return NotFound();

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.Register(request.Email, request.Password);

            if (!result.Succeeded) return BadRequest(new { message = "Registration failed", errors = result.Errors });

            return Ok(new { message = "User registered successfully" });
        }
    }
}
