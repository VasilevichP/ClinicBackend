namespace ProfilesService.Domain.Entities;

public class Patient
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public bool IsLinkedToAccount { get; private set; }
    public Guid? AccountId { get; private set; }
    
    private Patient(){}

    public Patient(string firstName, string lastName, string? middleName, DateTime dateOfBirth, bool isLinkedToAccount,
        Guid? accountId)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        DateOfBirth = dateOfBirth.Date;
        AccountId = accountId;
        IsLinkedToAccount = isLinkedToAccount;
    }
    
    public void LinkToAccount(Guid accountId)
    {
        AccountId = accountId;
        IsLinkedToAccount = true;
    }
}