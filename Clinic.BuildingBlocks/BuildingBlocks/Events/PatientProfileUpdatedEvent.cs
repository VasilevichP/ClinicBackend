namespace BuildingBlocks.Events;

public record PatientProfileUpdatedEvent
{
    public Guid AccountId { get; init; }
    public string PhoneNumber { get; init; } = string.Empty;
}