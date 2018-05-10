using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Poule.Entities;
using Poule.Services;

namespace Poule
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;

        public Startup(IConfiguration configuraton, IHostingEnvironment env)
        {
            _configuration = configuraton;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IGreeter, Greeter>();

            services.AddDbContext<PouleDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("Poule")));

            services.AddScoped<IUserData, SqlUserData>();
            services.AddScoped<IGameData, SqlGameData>();
            services.AddScoped<IPredictionData, SqlPredictionData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IConfiguration configuration,
            IGreeter greeter,
            ILogger<Startup> logger)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (_env.IsProduction())
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });

            }
            app.UseStaticFiles();
            app.UseMvc(ConfigureRoutes);
            
            app.Run(async (context) =>
            {
                var greeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"{greeting} ({_env.EnvironmentName})");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
