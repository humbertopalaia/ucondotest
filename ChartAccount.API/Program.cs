using ChartAccount.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Text;


internal class Program
{
    private static void Main(string[] args)
    {
        var environmentName = System.Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

        var path = Directory.GetCurrentDirectory();
        var configuration = new ConfigurationBuilder()
          .SetBasePath(path)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();


        var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .MinimumLevel.Debug()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Error)
                        .CreateLogger();

        Log.Logger = logger;



        CreateHostBuilder(args).Build().Run();

    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>()
                   .ConfigureKestrel((context, options) =>
                   {
                       options.AllowSynchronousIO = true;
                   });
               });

}