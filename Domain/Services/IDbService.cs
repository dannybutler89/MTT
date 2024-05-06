using Models.Request;
using Models.Response;

namespace Domain.Services
{
    public interface IDbService
    {
        Task<CompanyResponse> CompanyById(int companyId, CancellationToken ct);
        Task<IEnumerable<ClaimResponse>> ClaimsByCompanyId(int companyId, CancellationToken ct);
        Task<ClaimResponse> ClaimById(int claimId, CancellationToken ct);
        Task<ClaimResponse> UpdateClaim(UpdateClaimRequest request, CancellationToken ct);
    }
}
