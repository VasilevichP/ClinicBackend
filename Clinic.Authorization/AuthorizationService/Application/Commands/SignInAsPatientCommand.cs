using AuthorizationService.Application.DTO;
using MediatR;

namespace AuthorizationService.Application.Commands;

public record SignInAsPatientCommand(string Email,string Password):IRequest<TokenResponse>;