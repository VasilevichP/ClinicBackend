using MediatR;

namespace ProfilesService.Application.Commands;

public record LinkPatientProfileToExistingAccountCommand(Guid AccountId, Guid PatientId) : IRequest<bool>;