using Microsoft.EntityFrameworkCore;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.ApplicationServices.SpaceshipServices;
using Microsoft.Extensions.FileProviders;

namespace Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();
            builder.Services.AddScoped<IFileService, FileServices>();
            builder.Services.AddScoped<IRealEstateService, RealEstateService>();

            // Add services to the container.
            builder.Services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    options => options.EnableRetryOnFailure()
                );
            });

            var app = builder.Build();

            InitDatabse(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var fileProviderPath = FileServices.EnsureDirectoryExists(app.Environment.ContentRootPath);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(fileProviderPath),
                RequestPath = "/" + FileServices.DIR_NAME
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void InitDatabse(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var ctx = services.GetRequiredService<ShopContext>();
                    DbInitializer.InitDb(ctx);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                    throw;
                }
            }
        }
    }
}
