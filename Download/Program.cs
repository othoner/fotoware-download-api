using FWClient.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace DownloadAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddFotoWebServices(configuration);

                   services.AddHttpClient("FotoWebApi", httpClient =>
                   {
                       httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiBaseAddress"));
                   });

                   services.AddTransient<DownloadAPIApplication>();
                   services.AddSingleton<IConfiguration>(configuration);
               }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var downloadApiApplication = services.GetRequiredService<DownloadAPIApplication>();
                    var result = await downloadApiApplication.Run();

                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occured. " + ex.Message);
                }
            }
        }
    }
}
