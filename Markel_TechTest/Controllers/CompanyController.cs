using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Models.Request;

namespace Markel_TechTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IDbService _dbService;

        public CompanyController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("/{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> CompanyById([FromRoute] int companyId, CancellationToken ct)
        {
            return Ok(await _dbService.CompanyById(companyId, ct));
        }

        [HttpGet("claims/{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> ClaimsByCompanyId([FromRoute] int companyId, CancellationToken ct)
        {
            return Ok(await _dbService.ClaimsByCompanyId(companyId, ct));
        }

        [HttpGet("claim/{claimId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> ClaimById([FromRoute] int claimId, CancellationToken ct)
        {
            return Ok(await _dbService.ClaimById(claimId, ct));
        }

        [HttpPatch("claims")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status502BadGateway)]
        public async Task<IActionResult> UpdateClaim([FromBody] UpdateClaimRequest request, CancellationToken ct)
        {
            return Ok(await _dbService.UpdateClaim(request, ct));
        }
    }
}
