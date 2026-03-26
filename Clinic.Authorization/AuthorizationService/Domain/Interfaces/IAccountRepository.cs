using AuthorizationService.Domain.Entities;

namespace AuthorizationService.Domain.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(Account account, CancellationToken cancellationToken);
    Task UpdateAsync(Account account, CancellationToken cancellationToken);
    Task<Account?> GetByRefreshTokenAsync(string requestRefreshToken, CancellationToken cancellationToken);
}