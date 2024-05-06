using Domain.Helpers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Helpers
{
    public class DateHelperTests
    {
        private readonly DateHelper _dateHelper;

        public DateHelperTests()
        {
            _dateHelper = new DateHelper();
        }

        [Fact]
        public void IsFutureDate_WhenProvidedDateIsInTheFuture_ReturnsTrue()
        {
            // When - The date provided is in the future
            var actualResult = _dateHelper.IsFutureDate(DateTime.Now.AddYears(1));

            // Then - The response should be true
            actualResult.Should().BeTrue();
        }

        [Fact]
        public void IsFutureDate_WhenProvidedDateIsInThePast_ReturnsFalse()
        {
            // When - The date provided is in the past
            var actualResult = _dateHelper.IsFutureDate(DateTime.Now.AddDays(-1));

            // Then - The response should be false
            actualResult.Should().BeFalse();
        }

        [Fact]
        public void DateDifferenceInDays_WhenProvidedDateIsInThePast_ShouldReturn_CorrectDifferenceInDays()
        {
            int expectedDifferenceInDays = 1;

            // When - The date provided is in the past
            var actualResult = _dateHelper.DateDifferenceDays(DateTime.Now.AddDays(-1));

            // Then - The response should be false
            actualResult.Should().Be(expectedDifferenceInDays);
        }

        [Fact]
        public void DateDifferenceInDays_WhenProvidedDateIsInTheFuture_ShouldReturn_CorrectDifferenceInDays()
        {
            int expectedDifferenceInDays = 1;

            // When - The date provided is in the past
            var actualResult = _dateHelper.DateDifferenceDays(DateTime.Now.AddDays(1));

            // Then - The response should be false
            actualResult.Should().Be(expectedDifferenceInDays);
        }
    }
}
