using AuthorizationService.Application.DTO;
using MediatR;

namespace AuthorizationService.Application.Commands;

public record RefreshTokenCommand(string RefreshToken):IRequest<TokenResponse>;