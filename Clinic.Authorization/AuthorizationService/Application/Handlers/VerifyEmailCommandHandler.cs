using AuthorizationService.Application.Commands;
using AuthorizationService.Domain.Interfaces;
using MediatR;

namespace AuthorizationService.Application.Handlers;

public class VerifyEmailCommandHandler:IRequestHandler<VerifyEmailCommand,bool>
{
    private readonly IAccountRepository _accountRepository;

    public VerifyEmailCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(request.Id, cancellationToken);
        if (account == null) throw new Exception("Account not found");
        
        account.VerifyEmail();
        await _accountRepository.UpdateAsync(account, cancellationToken);
        return true;
    }
}