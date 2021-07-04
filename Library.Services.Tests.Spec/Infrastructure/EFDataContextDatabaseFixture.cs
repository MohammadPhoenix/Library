using Library.Persistance.EF;
using Xunit;

namespace Library.Services.Tests.Spec.Infrastructure
{
    [Collection(nameof(ConfigurationFixture))]
    public class EFDataContextDatabaseFixture : DatabaseFixture
    {
         ///readonly ConfigurationFixture _configuration;
        public EFDataContextDatabaseFixture(ConfigurationFixture configuration)
        {
             ///_configuration = configuration;
        }
        public EFDataContext CreateDataContext()
        {
            return new EFDataContext(@"server=PHOENIX\PHOENIX;database=Library;trusted_connection=true;");
        }
    }
}
