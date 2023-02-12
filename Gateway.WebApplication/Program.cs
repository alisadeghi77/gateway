using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gateway.Core.Extensions;

namespace Gateway.WebApplication
{
    public class Program
    {
        public static async Task Main()
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
                IEnumerable<string> sourceForExclude = new string[]
                {
                    "Serilog",
                    "System.Net.Http",
                    "Microsoft.AspNetCore"
                };
                SerilogExtension.CreateSeriLog(configuration, sourceForExclude);
                await CreateHostBuilder().Build().RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}