using AuthorizationService.Domain.ValueObjects;

namespace AuthorizationService.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public Role Role { get; private set; }
    public string? PhoneNumber { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public Guid? PhotoId { get; private set; }

    public Guid? CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }

    public Account(){}
    public Account(string email, string password, Role role)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        IsEmailVerified = false;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdatePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateRefreshToken(string refreshToken, DateTime expiryTime)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
        UpdatedAt = DateTime.UtcNow;
    }
}