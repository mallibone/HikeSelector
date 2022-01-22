using Xunit;

namespace HikeSelector.Test
{
    public class BootstrapperShould
    {
        [Fact]
        public void EnsureThatAllMappingsAreStrictlyConfigured()
        {
            var mapper = Bootstrapper.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
