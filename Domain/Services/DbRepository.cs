using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services
{
    public class DbRepository : IDbRepository
    {
        private readonly MarkelDbContext _dbContext;

        public DbRepository(MarkelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Claim?> FindClaimById(int claimId, CancellationToken ct)
        {
            return await _dbContext.Claims
                .Include(i => i.ClaimType)
                .FirstOrDefaultAsync(f => f.Id == claimId, ct);
        }

        public async Task<Company?> FindCompanyById(int companyId, CancellationToken ct)
        {
            return await _dbContext.Companies.FindAsync(companyId, ct);
        }

        public async Task<IEnumerable<Claim?>> SearchClaimsByCompanyId(int companyId, CancellationToken ct)
        {
            return await _dbContext.Companies
                .Include(i => i.Claims)
                    .ThenInclude(i => i.ClaimType)
                .Where(w => w.Id == companyId)
                .SelectMany(s => s.Claims)
                .ToListAsync(ct);
        }

        public async Task UpdateClaim(Claim request, CancellationToken ct)
        {
            _dbContext.Update(request);

            await _dbContext.SaveChangesAsync();
        }
    }
}
