
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OutlookCalendar.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureAppConfiguration((hostingContext, configBuilder) =>
           {
           })
            .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });
    }
}
