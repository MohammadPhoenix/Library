using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Library.Migration
{
    public class Program
    {

        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2014()
                    .WithGlobalConnectionString(@"server = . ; initial catalog = Library ; integrated security = true;")
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp(202106301429);
            //runner.MigrateDown(0);
        }
    }
}
