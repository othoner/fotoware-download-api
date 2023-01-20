using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DownloadAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHttpClient("FotoWebApi", httpClient =>
                   {
                       httpClient.BaseAddress = new Uri(configuration.GetValue<string>("ApiBaseAddress"));
                   });
                   services.AddHttpClient("InriverApi", httpClient =>
                   {
                       httpClient.BaseAddress = new Uri(configuration.GetValue<string>("InriverApiBaseAddress"));
                   });
                   services.AddTransient<DownloadAPIApplication>();
                   services.AddScoped<IAuthentication, Authentication>();
                   services.AddScoped<IDownload, Download>();
                   services.AddScoped<IRequestHandler, RequestHandler>();
                   services.AddSingleton<IConfiguration>(configuration);
               }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var downloadApiApplication = services.GetRequiredService<DownloadAPIApplication>();
                    var result = downloadApiApplication.Run();

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
