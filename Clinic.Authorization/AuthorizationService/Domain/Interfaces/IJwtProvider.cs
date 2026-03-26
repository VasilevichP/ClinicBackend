using AuthorizationService.Domain.Entities;

namespace AuthorizationService.Domain.Interfaces;

public interface IJwtProvider
{
    public string GenerateJwtToken(Account account);
    public string GenerateRefreshToken();   
}