using Models.Request;
using Models.Response;

namespace Domain.Services
{
    public interface IDbService
    {
        Task<CompanyResponse> CompanyById(CompanySearchRequest request, CancellationToken ct);
        Task<IEnumerable<ClaimResponse>> ClaimsByCompanyId(CompanySearchRequest request, CancellationToken ct);
        Task<ClaimResponse> ClaimById(ClaimSearchRequest request, CancellationToken ct);
        Task<ClaimResponse> UpdateClaim(UpdateClaimRequest request, CancellationToken ct);
    }
}
