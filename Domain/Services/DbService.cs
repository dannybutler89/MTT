using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Helpers;
using FluentValidation;
using Models.Request;
using Models.Response;

namespace Domain.Services
{
    public class DbService : IDbService
    {
        private readonly DateHelper _dateHelper;
        private readonly IMapper _mapper;
        private readonly IDbRepository _repository;
        private readonly IValidator<UpdateClaimRequest> _updateValidator;
        private readonly IValidator<CompanySearchRequest> _companySearchValidator;
        private readonly IValidator<ClaimSearchRequest> _claimSearchValidator;

        public DbService(DateHelper dateHelper, IMapper mapper, IDbRepository repository, IValidator<UpdateClaimRequest> updateValidator, IValidator<CompanySearchRequest> companyRequestValidator, IValidator<ClaimSearchRequest> claimRequestValidator)
        {
            _dateHelper = dateHelper;
            _mapper = mapper;
            _repository = repository;
            _updateValidator = updateValidator;
            _companySearchValidator = companyRequestValidator;
            _claimSearchValidator = claimRequestValidator;
        }

        public async Task<ClaimResponse> ClaimById(ClaimSearchRequest request, CancellationToken ct)
        {
            await _claimSearchValidator.ValidateAndThrowAsync(request);

            var claim = await _repository.FindClaimById(request.ClaimId, ct);

            if (claim is null)
            {
                throw new NotFoundException(nameof(claim), request.ClaimId);
            }

            var response = _mapper.Map<ClaimResponse>(claim);

            response.ClaimAgeDays = _dateHelper.DateDifferenceDays(claim.ClaimDate);

            return response;
        }

        public async Task<IEnumerable<ClaimResponse>> ClaimsByCompanyId(CompanySearchRequest request, CancellationToken ct)
        {
            await _companySearchValidator.ValidateAndThrowAsync(request);

            var claims = await _repository.SearchClaimsByCompanyId(request.CompanyId, ct);

            if (claims is null)
            {
                throw new NotFoundException(nameof(CompanyById), request.CompanyId);
            }

            var response = _mapper.Map<IEnumerable<ClaimResponse>>(claims);

            foreach (var claim in response)
            {
                claim.ClaimAgeDays = _dateHelper.DateDifferenceDays(claim.ClaimDate);
            }

            return response;
        }

        public async Task<CompanyResponse> CompanyById(CompanySearchRequest request, CancellationToken ct)
        {
            await _companySearchValidator.ValidateAndThrowAsync(request);

            var company = await _repository.FindCompanyById(request.CompanyId, ct);

            if (company is null)
            {
                throw new NotFoundException(nameof(Company), request.CompanyId);
            }

            var response = _mapper.Map<CompanyResponse>(company);

            response.HasActivePolicy = _dateHelper.IsFutureDate(response.InsuranceEndDate);

            return response;
        }

        public async Task<ClaimResponse> UpdateClaim(UpdateClaimRequest request, CancellationToken ct)
        {
            await _updateValidator.ValidateAndThrowAsync(request);

            var claim = await _repository.FindClaimById(request.Id, ct);

            if (claim is null)
            {
                throw new NotFoundException(nameof(Claim), request.Id);
            }

            _mapper.Map(request, claim);

            await _repository.UpdateClaim(claim, ct);

            var response = _mapper.Map<ClaimResponse>(claim);

            response.ClaimAgeDays = _dateHelper.DateDifferenceDays(response.ClaimDate);

            return response;
        }
    }
}
