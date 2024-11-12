using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ST10393673_CLDV6212_POE.Services;
using ST10393673_CLDV6212_POE.Data;
using Microsoft.EntityFrameworkCore;


namespace ST10393673_CLDV6212_POE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register services with IConfiguration
            builder.Services.AddSingleton<BlobService>(provider =>
                new BlobService(builder.Configuration));
            builder.Services.AddSingleton<FileService>(provider =>
                new FileService(builder.Configuration));
            builder.Services.AddSingleton<QueueService>(provider =>
                new QueueService(builder.Configuration));
            builder.Services.AddSingleton<TableService>(provider =>
                new TableService(builder.Configuration));

            // Add A database for storage.
            builder.Services.AddDbContext<ABCContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ABCConnection")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

    }
}
