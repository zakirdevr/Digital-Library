using DigitalLibrary.Data;
using DigitalLibrary.Helpers;
using DigitalLibrary.Models;
using DigitalLibrary.Repository;
using DigitalLibrary.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary
{
    public class Startup
    {
        private readonly IConfiguration _configuration = null;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookContext>(options => 
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<BookContext>();

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<BookContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<BookContext>().AddDefaultTokenProviders();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, 
                ApplicationUserClaimsPrincipalFactory>();
            
            services.Configure<IdentityOptions>(options=>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 1;

                options.SignIn.RequireConfirmedEmail = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(1);
            });

            services.ConfigureApplicationCookie(config =>
            {
                //config.LoginPath = "/login";
                config.LoginPath = _configuration["Application:LoginPath"];
            });

            services.AddControllersWithViews();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();

            //.AddViewOptions(options=> 
            //{ options.HtmlHelperOptions.ClientValidationEnabled = false; });
#endif
            services.Configure<NewBookAlertConfig>("InternalBook",_configuration.GetSection("NewBookAlert"));
            services.Configure<NewBookAlertConfig>("ThirdPartyBook",_configuration.GetSection("ThirdBookAlert"));
            services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware-1 ");
            //    await next();
            //    await context.Response.WriteAsync("Middleware-1-Response ");
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Middleware-2 ");
            //    await next();
            //    await context.Response.WriteAsync("Middleware-2-Response ");
            //});

            //app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync(" Hello World! ");
            //    });
            //});
            #endregion
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"MyStaticFiles")),
                RequestPath="/MyStaticFiles"
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    name: "MyArea",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

                //Conventional Routing (Not Recommend)
                //endpoints.MapControllerRoute(
                //    name:"Index",
                //    pattern:"index-zakir",
                //    defaults: new {controller="Home", action="Index"}
                //    );
            });
        }
    }
}
