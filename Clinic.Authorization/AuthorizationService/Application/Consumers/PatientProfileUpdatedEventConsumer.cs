using AuthorizationService.Domain.Interfaces;
using BuildingBlocks.Events;
using MassTransit;

namespace AuthorizationService.Application.Consumers;

public class PatientProfileUpdatedEventConsumer:IConsumer<PatientProfileUpdatedEvent>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<PatientProfileUpdatedEventConsumer> _logger;

    public PatientProfileUpdatedEventConsumer(IAccountRepository accountRepository, ILogger<PatientProfileUpdatedEventConsumer> logger)
    {
        _accountRepository = accountRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PatientProfileUpdatedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Обновление профиля для аккаунта {AccountId}", message.AccountId);

        var account = await _accountRepository.GetByIdAsync(message.AccountId, context.CancellationToken);
        
        if (account == null)
        {
            _logger.LogWarning("Аккаунт {AccountId} не найден", message.AccountId);
            return;
        }

        account.UpdatePhoneNumber(message.PhoneNumber);
        await _accountRepository.UpdateAsync(account, context.CancellationToken);
        
        _logger.LogInformation("Аккаунт {AccountId} обновлен", message.AccountId);
    }
}