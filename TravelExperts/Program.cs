using TravelExperts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


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
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //CreateHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
    }
}


