using AutoMapper;
using Domain.Helpers;
using FluentValidation;
using Models;
using Models.Request;

namespace Domain.Services
{
    //TODO: Error handling
    public class DbService : IDbService
    {
        private readonly DateHelper _dateHelper;
        private readonly IMapper _mapper;
        private readonly IDbRepository _repository;
        private readonly IValidator<UpdateClaimRequest> _validator;

        public DbService(DateHelper dateHelper, IMapper mapper, IDbRepository repository, IValidator<UpdateClaimRequest> validator)
        {
            _dateHelper = dateHelper;
            _mapper = mapper;
            _repository = repository;
            _validator = validator;
        }

        public async Task<ClaimResponse> ClaimById(int claimId, CancellationToken ct)
        {
            var claim = await _repository.FindClaimById(claimId, ct);

            if (claim is null)
            {
                throw new KeyNotFoundException($"No claim found for Id {claimId}");
            }

            var response = _mapper.Map<ClaimResponse>(claim);

            response.ClaimAgeDays = _dateHelper.DateDifferenceDays(claim.ClaimDate);

            return response;
        }

        public async Task<IEnumerable<ClaimResponse>> ClaimsByCompanyId(int companyId, CancellationToken ct)
        {
            var claims = await _repository.SearchClaimsByCompanyId(companyId, ct);

            if (claims is null)
            {
                throw new KeyNotFoundException($"No company found for Id {companyId}");
            }

            var response = _mapper.Map<IEnumerable<ClaimResponse>>(claims);

            foreach (var claim in response)
            {
                claim.ClaimAgeDays = _dateHelper.DateDifferenceDays(claim.ClaimDate);
            }

            return response;
        }

        public async Task<CompanyResponse> CompanyById(int companyId, CancellationToken ct)
        {
            var company = await _repository.FindCompanyById(companyId, ct);

            if (company is null)
            {
                throw new KeyNotFoundException($"No company found for Id {companyId}");
            }

            var response = _mapper.Map<CompanyResponse>(company);

            response.HasActivePolicy = _dateHelper.IsFutureDate(response.InsuranceEndDate);

            return response;
        }

        public async Task<ClaimResponse> UpdateClaim(UpdateClaimRequest request, CancellationToken ct)
        {
            _validator.ValidateAndThrow(request);

            var claim = await _repository.FindClaimById(request.Id, ct);

            if (claim is null)
            {
                throw new KeyNotFoundException($"No claim found for Id {request.Id}, update failed");
            }

            _mapper.Map(request, claim);

            await _repository.UpdateClaim(claim, ct);

            var response = _mapper.Map<ClaimResponse>(claim);

            response.ClaimAgeDays = _dateHelper.DateDifferenceDays(response.ClaimDate);

            return response;
        }
    }
}
