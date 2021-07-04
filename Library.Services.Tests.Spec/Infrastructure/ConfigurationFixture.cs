using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Library.Services.Tests.Spec.Infrastructure
{
    public class ConfigurationFixture
    {
        public TestSettings Value { get; private set; }
    }

    public class TestSettings
    {
        public string DbConnectionString { get; set; }
    }

    [CollectionDefinition(nameof(ConfigurationFixture), DisableParallelization = false)]
    public class ConfigurationCollectionFixture : ICollectionFixture<ConfigurationFixture>
    {
    }


}
