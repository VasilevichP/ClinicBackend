namespace AuthorizationService.Application.DTO;

public class TokenResponse
{
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}