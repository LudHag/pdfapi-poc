using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PdfApi.Framework.Configuration;

namespace PdfApi.Framework.Extensions
{
    public static class ServiceCollectionExtenstion
    {
        public static IServiceCollection AddHtmlToPdfConverterService(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            var context = new CustomAssemblyLoadContext();

            var root = env.ContentRootPath;
            var path = Path.Combine(root, "libwkhtmltox.dll");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(root, "libwkhtmltox.so");
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                path = Path.Combine(root, "libwkhtmltox.dylib");
            }

            context.LoadUnmanagedLibrary(path);

            return services;
        }
    }
}
