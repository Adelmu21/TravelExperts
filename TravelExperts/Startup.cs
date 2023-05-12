using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TravelExperts.Models;
using TravelExpertsData;
using Microsoft.Data.SqlClient;
using TravelExperts.Controllers;
using Microsoft.AspNetCore.Authentication.Cookies;

/*
Author: Adel M.
Instructor: Jolanta W.
Description: Website for the Inland Marina Company, that contains info about the company and ways to find out about their available products and also to contact them
Project: includes an ASP.NET MVC web page, that contains a database referenced to the website GUI as well as multiple controllers to navigate between different pages
and provide certain action to add functionality to the website, components such as buttons that provide direct access to homepage and the navbar does the same functionality
a slip page that includes all the unleased slips, with the ability to filter slips by Dock Type, concluding the project with an about page that contains info about how
to contact the company.
Date: April, 2023
*/
namespace TravelExperts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddControllersWithViews();
            services.AddDbContext<TravelExpertsContext>();
            services.AddScoped<PackagesController>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(opt => opt.LoginPath="/Account/Login");


            services.AddSession();
            services.AddControllersWithViews();

            //services.AddSession(options =>
            //{
            //    // Set a short timeout for easy testing
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});
            //services.AddDbContext<InlandMarinaContext>(
            //    options => options.UseSqlServer(
            //        Configuration.GetConnectionString("Server=localhost\\sqlexpress;Database=InlandMarina;Trusted_Connection=True;Encrypt=False")));
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseSession();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // map default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller = Packages}/ {action =Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Bookings}/{action=Index}/{id?}/{slug?}");


            });


        }
    }
}