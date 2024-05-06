using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services;
using FluentAssertions;
using FluentValidation;
using Models.Request;
using Models.Response;
using NSubstitute;
using Xunit;

namespace Tests.Services
{
    public class ClaimByIdTests
    {
        private readonly IDbService _service;
        private readonly DateHelper _dateHelper = Substitute.For<DateHelper>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly IDbRepository _repository = Substitute.For<IDbRepository>();
        private readonly IValidator<UpdateClaimRequest> _validator = Substitute.For<IValidator<UpdateClaimRequest>>();

        public ClaimByIdTests()
        {
            _service = new DbService(_dateHelper, _mapper, _repository, _validator);
        }

        [Fact]
        public async Task WhenIdIsNotFound_KeyNotFoundException_IsThrown()
        {
            //When - The claim id in the request does not exist and the repository response is null

            Claim claim = null;

            _repository.FindClaimById(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(claim);

            // Then - An exception of type KeyNotFoundException should be thrown
            var actualException = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.ClaimById(1, CancellationToken.None));

            // And - The exception message should be as expected
            actualException.Message.Should().Be("No claim found for Id 1");
        }

        [Fact]
        public async Task WhenIdIsFound_ResponseTypeIsClaimResponse()
        {
            //When - The claim id in the request does exist
            Claim claim = new Claim();

            ClaimResponse expectedResponse = new();

            _repository.FindClaimById(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(claim);
            _mapper.Map<ClaimResponse>(claim).Returns(expectedResponse);

            // Then - The returned type from the service should be the expected ClaimResponse
            var actualResponse = await _service.ClaimById(1, CancellationToken.None);

            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
