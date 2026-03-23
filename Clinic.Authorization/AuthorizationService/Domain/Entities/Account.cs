namespace AuthorizationService.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string? PhoneNumber { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public Guid? PhotoId { get; private set; }

    public Guid? CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Account(string email, string password)
    {
        Id = Guid.NewGuid();
        Email = email;
        Password = password;
        IsEmailVerified = false;
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
}