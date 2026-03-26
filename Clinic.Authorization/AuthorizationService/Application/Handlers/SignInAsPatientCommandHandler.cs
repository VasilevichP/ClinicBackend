using AuthorizationService.Application.Commands;
using AuthorizationService.Application.DTO;
using AuthorizationService.Domain.Interfaces;
using BuildingBlocks.Exceptions;
using MediatR;

namespace AuthorizationService.Application.Handlers;

public class SignInAsPatientCommandHandler : IRequestHandler<SignInAsPatientCommand, TokenResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public SignInAsPatientCommandHandler(IJwtProvider jwtProvider, IAccountRepository accountRepository,
        IPasswordHasher passwordHasher)
    {
        _jwtProvider = jwtProvider;
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<TokenResponse> Handle(SignInAsPatientCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (account == null)
        {
            throw new NotFoundException("Неправильный логин или пароль");
        }

        if (!_passwordHasher.VerifyPassword(account.Password, request.Password))
        {
            throw new NotFoundException("Неправильный логин или пароль");
        }

        var token = _jwtProvider.GenerateJwtToken(account);
        var refreshToken = _jwtProvider.GenerateRefreshToken();
        
        account.UpdateRefreshToken(refreshToken,DateTime.UtcNow.AddDays(7));
        await _accountRepository.UpdateAsync(account, cancellationToken);
        
        return new TokenResponse { JwtToken = token, RefreshToken = refreshToken };
    }
}