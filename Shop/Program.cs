using Microsoft.EntityFrameworkCore;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.ApplicationServices.SpaceshipServices;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Shop.Core.Domain;
using Shop.Hubs;

namespace Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.secrets.json");

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSignalR();

            builder.Services.AddScoped<ISpaceshipServices, SpaceshipServices>();
            builder.Services.AddScoped<IFileService, FileServices>();
            builder.Services.AddScoped<IRealEstateService, RealEstateService>();
            builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            builder.Services.AddScoped<IFreeGamesService, FreeGamesService>();
            builder.Services.AddScoped<ICocktailService, CocktailService>();
            builder.Services.AddScoped<IOpenWeatherService, OpenWeatherService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.SignIn.RequireConfirmedAccount = true;
                o.Password.RequiredLength = 3;
                o.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
                o.Lockout.MaxFailedAccessAttempts = 3;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
                .AddEntityFrameworkStores<ShopContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("CustomEmailConfirmation")
                .AddDefaultUI();

            //builder.Services.AddAuthentication().AddGoogle(opt =>
            //{
            //    var cfg = builder.Configuration;
            //    opt.ClientId = cfg["google-client-id"];
            //   opt.ClientSecret = cfg["google-secret"];
            //});

            builder.Services.ConfigureApplicationCookie(o =>
            {
                o.LoginPath = "/accounts/login";
                o.AccessDeniedPath = "/accounts/login";
            });

            builder.Services.AddSingleton<IEmailService, EmailService>(x =>
            {
                return new EmailService(
                    builder.Configuration.GetValue<string>("email-from"),
                    builder.Configuration.GetValue<string>("email-pass"));
            });

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

            app.MapHub<ChatHub>("/chatHub");

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
                    ctx.Database.Migrate();
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
