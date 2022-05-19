using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notes.Identity.Data;
using Notes.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Identity
{
    public class Startup
    {
        IConfiguration AppConfiguration { get; }

        public Startup(IConfiguration configuration) =>
            AppConfiguration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Получаю строку подключения из конфига
            var connectionString = AppConfiguration.GetValue<string>("DbConnection");

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>(config =>
            {
                // Длина пароля
                config.Password.RequiredLength = 4;
                // Обязательно ли должны быть цифры
                config.Password.RequireDigit = false;
                // Обязательные ли спецзнаки
                config.Password.RequireNonAlphanumeric = false;
                // Обязательны ли заглавные буквы
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            // Конфигурация IdentityServer
            services.AddIdentityServer()
                .AddAspNetIdentity<AppUser>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Notes.Identity.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // Для того, чтобы можно было использовать OAuth и OpenIdConnect
            app.UseIdentityServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
