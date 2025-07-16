using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventManagementSystem.Areas.Identity.Data;
using EventManagementSystem.Context;
using EventManagementSystem.Models;
using EventManagementSystem.Hubs;

var builder = WebApplication.CreateBuilder(args);

// ✅ Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// ✅ Register DbContexts
builder.Services.AddDbContext<IdentityDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<EventsDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CategoriesDbContext>(options =>
    options.UseSqlServer(connectionString));

// ✅ Configure Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<IdentityDBContext>()
.AddDefaultTokenProviders();

// ✅ Add MVC, Razor Pages, SignalR
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();

// ✅ Seed roles and default Admin user asynchronously
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();

        await SeedRolesAndAdminUserAsync(roleManager, userManager);
    }
    catch (Exception ex)
    {
        // Log exception or handle as needed
        Console.WriteLine($"Error during seeding: {ex.Message}");
        throw;
    }
}

async Task SeedRolesAndAdminUserAsync(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
{
    string[] roles = { "Admin", "Organizer", "Attendee" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    string adminEmail = "admin@ems.com";
    string adminPassword = "Admin@123";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            Name = "Admin"
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            Console.WriteLine("Failed to create admin user:");
            foreach (var error in result.Errors)
                Console.WriteLine($" - {error.Description}");
        }
    }
}

// ✅ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// ✅ Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.MapHub<BookingHub>("/bookingHub"); // SignalR endpoint

app.Run();
