using BuildingBlocks.Events;
using MassTransit;
using MediatR;
using ProfilesService.Application.Commands;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;

namespace ProfilesService.Application.Handlers;

public class CreatePatientProfileCommandHandler : IRequestHandler<CreatePatientProfileCommand, Guid>
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreatePatientProfileCommandHandler(IPatientRepository patientRepository, IPublishEndpoint publishEndpoint)
    {
        _patientRepository = patientRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Guid> Handle(CreatePatientProfileCommand request, CancellationToken cancellationToken)
    {
        var newPatient = new Patient(
            request.FirstName,
            request.LastName,
            request.MiddleName,
            request.DateOfBirth,
            true,
            request.AccountId);

        await _patientRepository.AddAsync(newPatient, cancellationToken);

        var eventMessage = new PatientProfileUpdatedEvent
            { AccountId = request.AccountId, PhoneNumber = request.PhoneNumber };
        await _publishEndpoint.Publish(eventMessage, cancellationToken);
        return newPatient.Id;
    }
}