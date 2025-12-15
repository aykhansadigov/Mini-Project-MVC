using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using MVC_Mini_Project.DAL;
using MVC_Mini_Project.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
   
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequireUppercase = true; 
    options.Password.RequireLowercase = true; 
    options.Password.RequireDigit = true; 

    options.User.RequireUniqueEmail = true;
   
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();



var app = builder.Build();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();