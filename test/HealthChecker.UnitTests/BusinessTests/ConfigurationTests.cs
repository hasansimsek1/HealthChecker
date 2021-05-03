using HealthChecker.Business.Configurations;
using Xunit;

namespace HealthChecker.UnitTests.BusinessTests
{
    public class ConfigurationTests
    {
        [Fact]
        public void AutoMapperConfiguration_ShouldReturnMappingConfigWith6Mappings()
        {
            int expectedValue = 6;

            AutoMapperConfiguration autoMapperConfiguration = new AutoMapperConfiguration();
            var config = autoMapperConfiguration.Configure();
            var typeMaps = config.GetAllTypeMaps();

            Assert.Equal(expectedValue, typeMaps.Length);
        }
    }
}
