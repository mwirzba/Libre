using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Libre.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System;
using Libre.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Libre
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                 .AddDefaultUI()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();


            //services.AddTransient<CustomEmailConfirmationTokenProvider<IdentityUser>>();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<EmailOptions>(Configuration);


            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthorization(options => {
                options.AddPolicy("readpolicy",
                    builder => builder.RequireRole("Admin", "Moderator", "User"));
                options.AddPolicy("writepolicy",
                    builder => builder.RequireRole("Admin", "Moderator"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            CreateRoles(serviceProvider).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            Task<IdentityResult> roleResult;
            string adminEmail = "admin@libre.com";
            string moderatorEmail = "moderator@libre.com";
            string userEmail = "user@libre.com";

            //Check that there is an Admin role and create if not
            Task<bool> hasAdminRole = roleManager.RoleExistsAsync("Admin");
            hasAdminRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Admin"));
                roleResult.Wait();
            }

            //Check that there is an Moderator role and create if not
            Task<bool> hasModeratorRole = roleManager.RoleExistsAsync("Moderator");
            hasModeratorRole.Wait();

            if (!hasModeratorRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Moderator"));
                roleResult.Wait();
            }

            //Check that there is an User role and create if not
            Task<bool> hasUserRole = roleManager.RoleExistsAsync("User");
            hasUserRole.Wait();

            if (!hasUserRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("User"));
                roleResult.Wait();
            }

            //Check if the admin user exists and create it if not
            //Add to the Admin role

            var _testAdmin = await userManager.FindByEmailAsync(adminEmail);
            if (_testAdmin == null)
            {
                var user = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            //Check if the moderator user exists and create it if not
            //Add to the Moderator role

            var _testModerator = await userManager.FindByEmailAsync(moderatorEmail);
            if (_testModerator == null)
            {
                var user = new IdentityUser { UserName = moderatorEmail, Email = moderatorEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, "Moderator123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Moderator");
                }
            }

            //Check if the user exists and create it if not
            //Add to the user role

            var _testUser = await userManager.FindByEmailAsync(userEmail);
            if (_testUser == null)
            {
                var user = new IdentityUser { UserName = userEmail, Email = userEmail, EmailConfirmed = true };
                var result = await userManager.CreateAsync(user, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                }
            }

        }
    }
}
