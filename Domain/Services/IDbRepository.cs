using Domain.Entities;

namespace Domain.Services
{
    public interface IDbRepository
    {
        Task<Claim?> FindClaimById(int claimId, CancellationToken ct);
        Task<IEnumerable<Claim?>> SearchClaimsByCompanyId(int companyId, CancellationToken ct);
        Task<Company?> FindCompanyById(int companyId, CancellationToken ct);
        Task UpdateClaim(Claim claim, CancellationToken ct);
    }
}
