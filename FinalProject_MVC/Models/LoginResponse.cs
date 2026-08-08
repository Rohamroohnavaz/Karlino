namespace FinalProject_MVC.Models
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }
        public string Role { get; set; }
        public string CompanyId { get; set; }
    }
}
