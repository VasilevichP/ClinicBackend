using BuildingBlocks.Exceptions;
using MediatR;
using ProfilesService.Application.Commands;
using ProfilesService.Domain.Interfaces;

namespace ProfilesService.Application.Handlers;

public class LinkPatientProfileToExistingAccountCommandHandler: IRequestHandler<LinkPatientProfileToExistingAccountCommand, bool>
{
    private readonly IPatientRepository _repository;

    public LinkPatientProfileToExistingAccountCommandHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(LinkPatientProfileToExistingAccountCommand request, CancellationToken cancellationToken)
    {
        var patient = await _repository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient == null) throw new NotFoundException("Профиль не найден");

        if (patient.IsLinkedToAccount) throw new DomainException("Профиль уже привязан к другому аккаунту");

        patient.LinkToAccount(request.AccountId);
        
        await _repository.UpdateAsync(patient, cancellationToken);

        // TODO: Отправить в RabbitMQ событие для обновления профиля в сервисе Authorization

        return true;
    }
}