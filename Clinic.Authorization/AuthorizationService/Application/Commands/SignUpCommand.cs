using MediatR;

namespace AuthorizationService.Application.Commands;

public record SignUpCommand(string Email, string Password, string ReEnteredPassword):IRequest<Guid>;