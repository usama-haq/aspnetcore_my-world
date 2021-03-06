﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWorld.Models;
using MyWorld.Services;
using MyWorld.ViewModels;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace MyWorld
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json");

            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
                {
                    if (_env.IsProduction())
                    {
                        config.Filters.Add(new RequireHttpsAttribute());
                    }
                })
            .AddJsonOptions(opt =>
               {
                   opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               });
            services.AddLogging();
            services.AddIdentity<WorldUser, IdentityRole>(
                config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequiredLength = 8;
                    config.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                    config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = async ctx =>
                        {
                            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                            {
                                ctx.Response.StatusCode = 401;
                            }
                            else
                            {
                                ctx.Response.Redirect(ctx.RedirectUri);
                            }
                            await Task.Yield();
                        }
                    };
                }
                ).AddEntityFrameworkStores<WorldContext>();

            services.AddDbContext<WorldContext>();

            services.AddScoped<IWorldRepository, WorldRepository>();
            services.AddScoped<GeoCoordsService>();
            services.AddTransient<WorldContextSeedData>();

            services.AddScoped<IMailService, DebugMailService>();
            services.AddSingleton(_config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // !!! Order is important !!!
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, WorldContextSeedData seeder)
        {
            loggerFactory.AddDebug(LogLevel.Warning);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
            }

            app.UseStaticFiles();   // Server static files from wwwroot directory

            app.UseIdentity();

            Mapper.Initialize(config =>
            {
                config.CreateMap<TripViewModel, Trip>().ReverseMap();
                config.CreateMap<StopViewModel, Stop>().ReverseMap();
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                    );
            });

            seeder.EnsureSeedData().Wait();
        }
    }
}