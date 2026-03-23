using MediatR;

namespace ProfilesService.Application.Commands;

public record CreatePatientProfileCommand(
    Guid AccountId, 
    string FirstName, 
    string LastName, 
    string? MiddleName, 
    string PhoneNumber, 
    DateTime DateOfBirth) : IRequest<Guid>;