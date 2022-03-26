using Common.PdfUtil.Core.Implementations;
using Common.PdfUtil.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PdfUtil.Core;
using System;
namespace PdfUtil.Extender
{
    internal static class ServiceExtender
    {
        internal static void AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((_, services) =>
            {
                services.AddSingleton<IDocOperations, ProtectPdf>();
                services.AddSingleton<IDocOperations, UnlockPdf>();
                services.AddSingleton<ProcessDocument>();
                services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            });
        }
    }
}
