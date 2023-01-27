using System;
using System.Threading.Tasks;
using Download.Crawl;
using Download.FileManager;
using FWClient.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Download
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   // Register FotoWeb services.
                   services.AddFotoWebServices(configuration);

                   services.AddTransient<DownloadApiSample>();
                   services.AddSingleton<IConfiguration>(configuration);
                   services.AddScoped<IFileManager, FileManager.FileManager>();
                   services.AddScoped<ICrawlService, CrawlService>();
               }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var downloadApiApplication = services.GetRequiredService<DownloadApiSample>();
                    var result = await downloadApiApplication.Run();

                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred. " + ex.Message);
                }
            }
        }
    }
}