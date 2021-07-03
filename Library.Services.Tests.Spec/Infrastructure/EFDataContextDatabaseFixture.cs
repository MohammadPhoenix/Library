using Library.Persistance.EF;
using Xunit;

namespace Library.Services.Tests.Spec.Infrastructure
{
    [Collection(nameof(ConfigurationFixture))]
    public class EFDataContextDatabaseFixture : DatabaseFixture
    {
        // readonly ConfigurationFixture _configuration;

        public EFDataContextDatabaseFixture(ConfigurationFixture configuration)
        {
            //  _configuration = configuration;
        }

        public EFDataContext CreateDataContext()
        {
            //return new ApplicationDbContext(@"server=PHOENIX\PHOENIX;initial catalog=MyLittleShops;integrated security=True;");
            return new EFDataContext(@"server=.;database=Library;trusted_connection=true;");
        }
    }
}
