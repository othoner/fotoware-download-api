using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UploadAPI
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {

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
                       httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                   });
                   services.AddTransient<UploadAPIApplication>();
                   services.AddScoped<IAuthentication, Authentication>();
                   services.AddScoped<IUpload, Upload>();
                   services.AddScoped<IInputHandler, InputHandler>();
                   services.AddScoped<IRequestHandler, RequestHandler>();
                   services.AddSingleton<IConfiguration>(configuration);
               }).UseConsoleLifetime();

            /*
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });
            */

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var uploadApiApplication = services.GetRequiredService<UploadAPIApplication>();
                    var result = uploadApiApplication.Run();

                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }

            return 0;
        }
    }
}
