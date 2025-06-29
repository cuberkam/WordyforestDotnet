namespace WordyforestDotnet.EntityLayer.DTOs.Auth
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public  string? AccessToken { get; set; }
        public  string? RefreshToken { get; set; }
        public required bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public int? ExpiresIn { get; set; }
    }
}