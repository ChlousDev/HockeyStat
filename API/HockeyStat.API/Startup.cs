using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HockeyStat.Model.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Formatting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace HockeyStat.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                //.AddUserSecrets()
                .AddJsonFile("secretsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // Add framework services.
            services.AddMvc().AddWebApiConventions().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddRouting();

            string connectionString =this.Configuration.GetValue<string>("ConnectionString");
            services.AddDbContext<HockeyStatDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddTransient<HockeyStatDataAccess, HockeyStatDataAccess>();

            services.Configure<ConfigurationOptions>(Configuration);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Set-Cookie")
                .AllowCredentials());

            // Route all unknown requests to app root
            app.Use(async (context, next) =>
            {
                await next();

                // If there's no available file and the request doesn't contain an extension, we're probably trying to access a page.
                // Rewrite request to use app root
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html"; // Put your Angular root page here 
                    context.Response.StatusCode = 200; // Make sure we update the status code, otherwise it returns 404
                    await next();
                }
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "HockeyStatAuthorization",
                LoginPath = new PathString("/api/User/Unauthorized/"),
                AccessDeniedPath = new PathString("/api/User/Unauthorized/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                CookieName = "HockeyStatAuthorization",
                Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    },
                    OnRedirectToAccessDenied = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden; 
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                }
            });

            app.UseMvc();
            app.UseStaticFiles();
        }
    }
}
