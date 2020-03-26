using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertismentPlatform.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Identity;
using AdvertismentPlatform.Models.MySqlRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AdvertismentPlatform.Security;

namespace AdvertismentPlatform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

      
        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add global authorization filter, requires user to be logged in to use app
            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddDbContextPool<AppDbContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("adplatform")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>( options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 4;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddTokenProvider<CustomEmailConfirmationTokenProvider<ApplicationUser>>("CustomEmailConfirmation");


            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "824631466201-rsmg37ukppkgvr2i5d9r4tgjljpsc5gi.apps.googleusercontent.com";
                    options.ClientSecret = "aaRirFRgJSIbJlkpYmWTYeix";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "872704303179040";
                    options.AppSecret = "e09b7365a46ec7b5f6c2fb4f1dc91b77";
                });

            // Change all token lifetime to 10h 
            //services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(10));

            // after creating custom TokenProvider for email confirmation, configure lifetime to 3 days
            services.Configure<CustomEmailConfirmationTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromDays(3));

            services.AddScoped<IItemRepository<ItemCategory>, BaseItemRepository>();
            services.AddScoped<IAutoItemRepository, AutoRepository>();
            services.AddScoped<IBikeItemRepository, BikeRepository>();
            services.AddScoped<IAdvertismentRepository, AdvertismentRepository>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<DataProtectionPurposeStrings>();
        }




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
