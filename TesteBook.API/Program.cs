using MaximaTechCriptografia.Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace TesteBook.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            UtilitarioLogger.PathLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOGS");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
