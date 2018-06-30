using Poule.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.IdentityModel.Protocols;
using Poule.Services;
using StackExchange.Redis;

namespace Poule
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                var config = host.Services.GetRequiredService<IConfiguration>();
                // Set password with the Secret Manager tool.
                // dotnet user-secrets set SeedUserPW <pw>

                var testUserPw = config["SeedUserPW"];
                if (testUserPw == null)
                {
                    testUserPw = Environment.GetEnvironmentVariable("POULE_SEED_USER_PASSWORD");
                }
                try
                {
                    SeedData.Initialize(services, testUserPw).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw ex;
                }


                var redis = config["Redis:host"];
                LazyConnection = new Lazy<ConnectionMultiplexer>(() =>  ConnectionMultiplexer.Connect(redis));
         
                var predictionData = services.GetRequiredService<IPredictionData>();
                predictionData.InitCache();



            }

            host.Run();

            LazyConnection.Value.Dispose();
        }

        public static Lazy<ConnectionMultiplexer> LazyConnection;
        
    
        public static ConnectionMultiplexer Connection => LazyConnection.Value;


        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}