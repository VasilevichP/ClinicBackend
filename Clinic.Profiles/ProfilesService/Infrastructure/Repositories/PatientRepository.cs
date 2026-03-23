using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Entities;
using ProfilesService.Domain.Interfaces;
using ProfilesService.Infrastructure.Context;

namespace ProfilesService.Infrastructure.Repositories;

public class PatientRepository: IPatientRepository
{
    private readonly ProfilesDbContext _context;

    public PatientRepository(ProfilesDbContext context) => _context = context;

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Patients.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<Patient>> GetUnlinkedProfilesAsync(CancellationToken cancellationToken) =>
        await _context.Patients.Where(x => !x.IsLinkedToAccount).ToListAsync(cancellationToken);

    public async Task AddAsync(Patient patient, CancellationToken cancellationToken)
    {
        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }
}