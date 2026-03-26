using AuthorizationService.Application.Commands;
using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.Interfaces;
using AuthorizationService.Domain.ValueObjects;
using BuildingBlocks.Exceptions;
using MediatR;

namespace AuthorizationService.Application.Handlers;

public class SignUpCommandHandler:IRequestHandler<SignUpCommand, Guid>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordHasher _passwordHasher;

    public SignUpCommandHandler(IAccountRepository accountRepository, IEmailSender emailSender, IPasswordHasher passwordHasher)
    {
        _accountRepository = accountRepository;
        _emailSender = emailSender;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        
        var existingUser = await _accountRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (existingUser != null)
        {
            throw new DomainException("Введенный Email уже используется");
        }
        var hashedPassword = _passwordHasher.HashPassword(request.Password);
        var account = new Account(request.Email, hashedPassword, Role.Patient);

        await _accountRepository.AddAsync(account, cancellationToken);
        
        //TODO
        //придется менять ссылку: она должна будет вести на страницу с созданием профиля клиента. 
        //верифай имейла: при переходе в профиль будет проверяться верифайд ли имейл, если нет - то метод вызовется (так себе варик)
        var link = $"http://localhost:5000/api/auth/verify?accountId={account.Id}";
        await _emailSender.SendConfirmationLinkAsync(account.Email, link);

        return account.Id;
    }
}