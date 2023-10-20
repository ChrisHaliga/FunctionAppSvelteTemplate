using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(FunctionAppSvelteTemplate.Startup))]

namespace FunctionAppSvelteTemplate
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder
                .SetBasePath(builder.GetContext().ApplicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var myLocalSetting = builder.GetContext().Configuration["Values:MyLocalSetting"];
            //builder.Services.AddSingleton(myClass(myLocalSetting));
        }

    }
}
