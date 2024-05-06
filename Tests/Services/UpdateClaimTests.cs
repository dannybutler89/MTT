using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Services;
using Domain.Validation;
using FluentAssertions;
using FluentValidation;
using Models;
using Models.Request;
using NSubstitute;
using Xunit;

namespace Tests.Services
{
    public class UpdateClaimTests
    {
        private readonly IDbService _service;
        private readonly DateHelper _dateHelper = Substitute.For<DateHelper>();
        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly IDbRepository _repository = Substitute.For<IDbRepository>();
        private readonly UpdateClaimRequestValidator _validator;

        public UpdateClaimTests()
        {
            _validator = new UpdateClaimRequestValidator();
            _service = new DbService(_dateHelper, _mapper, _repository, _validator);
        }

        [Fact]
        public async Task WhenRequestIsNotValid_ValidationException_IsThrown()
        {
            // When - The request has a negative IncurredLoss value
            UpdateClaimRequest request = new()
            {
                Id = 1,
                AssuredName = "Test",
                ClaimDate = DateTime.Today,
                Closed = false,
                IncurredLoss = -1m,
                LossDate = DateTime.Today,
                Ucr = "Test"
            };

            // Then - The validator should throw an exception
            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await _service.UpdateClaim(request, CancellationToken.None));

            exception.Message.Should().Contain("IncurredLoss: 'Incurred Loss' must be greater than '0'.");
        }

        [Fact]
        public async Task WhenRequestIsValid_NoExceptionIsThrown_And_CorrectTypeIsReturned()
        {
            // When - The request is valid
            UpdateClaimRequest request = new()
            {
                Id = 1,
                AssuredName = "Test",
                ClaimDate = DateTime.Today,
                Closed = false,
                IncurredLoss = 10m,
                LossDate = DateTime.Today,
                Ucr = "Test"
            };

            ClaimResponse expectedResponse = new();

            _mapper.Map<ClaimResponse>(Arg.Any<Claim>()).Returns(expectedResponse);
            _repository.FindClaimById(Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns(new Claim());

            // Then - The then response type should be ClaimResponse
            var actualResponse = await _service.UpdateClaim(request, CancellationToken.None);

            actualResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public async Task WhenClaimIdIsNotFound_UpdateClaimIsNotCalled_KeyNotFoundException_IsThrown()
        {
            // When - The request is valid
            UpdateClaimRequest request = new()
            {
                Id = 1,
                AssuredName = "Test",
                ClaimDate = DateTime.Today,
                Closed = false,
                IncurredLoss = 10m,
                LossDate = DateTime.Today,
                Ucr = "Test"
            };

            // Then - An exception of type KeyNotFoundException should be thrown
            var actualException = await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.ClaimById(1, CancellationToken.None));

            // And - The exception message should be as expected
            actualException.Message.Should().Be("No claim found for Id 1");

            await _repository.Received(0).UpdateClaim(Arg.Any<Claim>(), Arg.Any<CancellationToken>());
            _mapper.Received(0).Map<ClaimResponse>(Arg.Any<Claim>());
        }
    }
}
