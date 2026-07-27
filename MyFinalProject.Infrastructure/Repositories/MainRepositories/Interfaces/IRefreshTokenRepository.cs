using MyFinalProject.Domain.Entities.MainModels;
using MyFinalProject.Infrastructure.Repositories.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinalProject.Infrastructure.Repositories.MainRepositories.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

        Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default);

        Task<RefreshToken?> GetTokenWithUserAsync(string token, CancellationToken cancellationToken = default);

        Task<List<RefreshToken>> GetUserTokensAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<bool> HasAnyActiveTokenAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<bool> IsTokenActiveAsync(string token, CancellationToken cancellationToken = default);

        Task RevokeAsync(RefreshToken refreshToken, string? replacedByToken = null,
            string? revokeReason = null, CancellationToken cancellationToken = default);

        Task RevokeAllUserTokensAsync(Guid userId, string? revokeReason = null,
         CancellationToken cancellationToken = default);

        Task RemoveExpiredTokensAsync(CancellationToken cancellationToken = default);
    }
}
