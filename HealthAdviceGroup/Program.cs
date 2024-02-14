using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HealthAdviceGroup.Areas.Identity.Data;
using HealthAdviceGroup.Data;

namespace GibJohn
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Initialises the database and configures application
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

            // Adds the DbContext to the service container with SQL Server as the database provider
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Configures default identity options and adds identity services to the service container
            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Adds HTTPS redirection and static files middleware to the pipeline
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); // Enables authentication middleware
            app.UseAuthorization(); // Enables authorization middleware

            // Maps controller routes and Razor Pages
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            // Check if the role already exists, if not create the roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "User", "Management" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            DotNetEnv.Env.Load(); // Load environment variables from a .env file
            app.Run(); // Runs the application
        }
    }
}
