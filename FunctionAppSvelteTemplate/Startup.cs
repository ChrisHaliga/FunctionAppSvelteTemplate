using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

[assembly: FunctionsStartup(typeof(FunctionAppSvelteTemplate.Startup))]

namespace FunctionAppSvelteTemplate
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var env = builder.GetContext();
            builder.ConfigurationBuilder
                .SetBasePath(builder.GetContext().ApplicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine(env.ApplicationRootPath, $"appsettings.{env.EnvironmentName}.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var rootFolder = builder.GetContext().Configuration["Values:RootFolder"];
            builder.Services.AddSingleton(rootFolder);
        }

    }
}
