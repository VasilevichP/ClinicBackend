namespace AuthorizationService.Domain.Interfaces;

public interface IEmailSender
{
    Task SendConfirmationLinkAsync(string email, string link);
}