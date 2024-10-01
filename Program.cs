using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using CldDev6212.Poe.AzServices;

namespace prrgrm
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddSingleton(new AzureStorageService(Configuration.GetConnectionString("AzureStorage")));
        }


    }
    public class Program
    {
       
                
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllersWithViews();
        /*
        builder.Services.AddScoped<blobStorage>();
        builder.Services.AddScoped<tableService>();
        builder.Services.AddScoped<queueService>();
        builder.Services.AddScoped<fileService>();
        */
        builder.Services.AddSingleton<tableService>();
        builder.Services.AddSingleton<blobStorage>();
        builder.Services.AddSingleton<fileService>();
        builder.Services.AddSingleton<queueService>();

            builder.WebHost.UseUrls("https://localhost:7139", "http://localhost:5000");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
           //app.UseAuthentication();
            app.UseAuthorization();
            // IServiceCollection.AddAuthorization;



            //app.MapControllers();
           

                app.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");

               
            
                app.Run();
        }
    }
}