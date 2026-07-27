using Microsoft.EntityFrameworkCore;
using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.Generics;
using MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Repos
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        private readonly FinalDbContext _dbContext;

        public RefreshTokenRepository(FinalDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<RefreshToken>().AddAsync(refreshToken, cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<RefreshToken>()
                .FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
        }

        public async Task<RefreshToken?> GetTokenWithUserAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<RefreshToken>()
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token, cancellationToken);
        }

        public async Task<List<RefreshToken>> GetUserTokensAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<RefreshToken>()
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasAnyActiveTokenAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<RefreshToken>()
                .AnyAsync(r => r.UserId == userId && !r.IsRevoked
                && r.ExpiresAt > DateTime.UtcNow, cancellationToken);
        }

        public async Task<bool> IsTokenActiveAsync(string token, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<RefreshToken>()
               .AnyAsync(r => r.Token == token && !r.IsRevoked
               && r.ExpiresAt > DateTime.UtcNow, cancellationToken);
        }

        public Task RevokeAsync(RefreshToken refreshToken, string? replacedByToken = null, string? revokeReason = null, CancellationToken cancellationToken = default)
        {
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;
            refreshToken.ReplacedByToken = replacedByToken;
            refreshToken.RevokeReason = revokeReason;

            _dbContext.Set<RefreshToken>().Update(refreshToken);
            return Task.CompletedTask;
        }

        public async Task RevokeAllUserTokensAsync(Guid userId,string? revokeReason = null,
        CancellationToken cancellationToken = default)
        {
            var activeTokens = await _dbContext.Set<RefreshToken>()
                .Where(x => x.UserId == userId &&
                            !x.IsRevoked &&
                            x.ExpiresAt > DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            foreach (var token in activeTokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
                token.RevokeReason = revokeReason;
            }
        }

        public async Task RemoveExpiredTokensAsync(CancellationToken cancellationToken = default)
        {
            var expiredTokens = await _dbContext.Set<RefreshToken>()
                .Where(x => x.ExpiresAt <= DateTime.UtcNow)
                .ToListAsync(cancellationToken);

            _dbContext.Set<RefreshToken>().RemoveRange(expiredTokens);
        }
    }
}
