using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.Interfaces;
using AuthorizationService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AuthDbContext _context;

    public AccountRepository(AuthDbContext context) => _context = context;

    public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

    public async Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        await _context.Accounts.FirstOrDefaultAsync(a => a.Email == email, cancellationToken);

    public async Task AddAsync(Account account, CancellationToken cancellationToken)
    {
        await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Account account, CancellationToken cancellationToken)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Account?> GetByRefreshTokenAsync(string requestRefreshToken, CancellationToken cancellationToken)
    {
        return await _context.Accounts.FirstOrDefaultAsync(a => a.RefreshToken == requestRefreshToken, cancellationToken);
    }
}