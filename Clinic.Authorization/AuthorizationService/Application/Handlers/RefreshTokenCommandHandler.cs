using AuthorizationService.Application.Commands;
using AuthorizationService.Application.DTO;
using AuthorizationService.Domain.Interfaces;
using BuildingBlocks.Exceptions;
using MediatR;

namespace AuthorizationService.Application.Handlers;

public class RefreshTokenCommandHandler:IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IJwtProvider _jwtProvider;

    public RefreshTokenCommandHandler(IJwtProvider jwtProvider, IAccountRepository accountRepository)
    {
        _jwtProvider = jwtProvider;
        _accountRepository = accountRepository;
    }

    public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);

        if (account == null)
            throw new NotFoundException("Пользователь не найден");
        if (account.RefreshTokenExpiryTime < DateTime.UtcNow)
            throw new DomainException("Время сеанса истекло. Требуется перезайти в систему");
        
        var newJwtToken = _jwtProvider.GenerateJwtToken(account);
        var newRefreshToken = _jwtProvider.GenerateRefreshToken();

        account.UpdateRefreshToken(newRefreshToken, DateTime.UtcNow.AddDays(7));
        await _accountRepository.UpdateAsync(account, cancellationToken);

        return new TokenResponse
        {
            JwtToken = newJwtToken,
            RefreshToken = newRefreshToken
        };
    }
}