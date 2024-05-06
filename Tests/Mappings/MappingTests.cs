using AutoMapper;
using Domain.Maps;
using Xunit;

namespace Tests.Mappings
{
    public class MappingTests
    {
        [Fact]
        public void Assert_MappingProfiles_AreValid()
        {
            // Configure the mapper with all maps in the assembly
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(ClaimMappings).Assembly);
            });

            // Assert that the configuration is valid
            config.AssertConfigurationIsValid();
        }
    }
}
