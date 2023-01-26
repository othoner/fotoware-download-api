using FWClient.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Upload.UserInput;

namespace Upload
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
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

                   services.AddTransient<UploadApiSample>();

                   services.AddScoped<IInputHandler, InputHandler>();
                   services.AddSingleton<IConfiguration>(configuration);
               }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var uploadApiApplication = services.GetRequiredService<UploadApiSample>();
                    var result = await uploadApiApplication.Run();

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