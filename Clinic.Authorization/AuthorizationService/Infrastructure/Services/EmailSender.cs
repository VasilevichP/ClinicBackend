using AuthorizationService.Domain.Interfaces;

namespace AuthorizationService.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public Task SendConfirmationLinkAsync(string email, string link)
    {
        Console.WriteLine("email: " + email);
        Console.WriteLine("link: " + link);
        return Task.CompletedTask;
    }
}