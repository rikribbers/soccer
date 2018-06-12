using Poule.Authorization;
using Poule.Data;
using Poule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Poule
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Poule")));

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.SignIn.RequireConfirmedEmail = true;
                    // Password settings
                    config.Password.RequireDigit = false;
                    config.Password.RequiredLength = 8;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequiredUniqueChars = 6;
                 })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserData, SqlUserData>();
            services.AddScoped<IGameData, SqlGameData>();
            services.AddScoped<IPredictionData, SqlPredictionData>();

            services.AddSingleton<IScoreValidator,
                ScoreValidator>();
            services.AddSingleton<IScoreCalculator,
                ScoreCalculator>();


            var skipHTTPS = Configuration.GetValue<bool>("LocalTest:skipHTTPS");
            // requires using Microsoft.AspNetCore.Mvc;
            services.Configure<MvcOptions>(options =>
            {
                // Set LocalTest:skipHTTPS to true to skip SSL requrement in 
                // debug mode. This is useful when not using Visual Studio.
                if (Environment.IsDevelopment() && !skipHTTPS)
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            });

            services.AddMvc();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
            });

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IGameConverter, GameConverter>();
            services.AddSingleton<IPredictionConverter, PredictionConverter>();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });


            // Authorization handlers.
            // LATER ADD MORE OF THEM
            services.AddSingleton<IAuthorizationHandler,
                PouleAdminPredictionAuthorizationHandler>();
        

            // Mail
            var smtpConfig = Configuration.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            var smtpPassword = Configuration["SMTPUserPW"];

            if (smtpPassword == null)
            {
                smtpPassword = System.Environment.GetEnvironmentVariable("POULE_SMTP_PASSWORD");
            }
            smtpConfig.SmtpPassword = smtpPassword;
            services.AddSingleton<IEmailConfiguration>(smtpConfig);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(ConfigureRoutes);

        }
    
        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
