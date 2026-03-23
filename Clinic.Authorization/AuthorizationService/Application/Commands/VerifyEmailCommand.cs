using MediatR;

namespace AuthorizationService.Application.Commands;

public record VerifyEmailCommand(Guid Id): IRequest<bool>;