using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfUtil.Core;
using PdfUtil.Extender;
using System.IO;
using System.Threading.Tasks;

namespace PdfUtil
{
    public class Program
    {


        public static IConfiguration Configuration { get; private set; }

        public async static Task<int> Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", false, false);

            Configuration = builder.Build();

            var hostBuilder = Host.CreateDefaultBuilder(args);
            hostBuilder.AddServices();

            using IHost host = hostBuilder.Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<ProcessDocument>();
                context.Run();
            };
            await host.RunAsync();
            return 1;
        }
    }
}
