using Microsoft.EntityFrameworkCore;
using DAL;
using Logic.Interface;
using Logic.Services;
using DAL.Interfaces;
using DAL.repository;
using Logic.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IUserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IDoorRepository>(provider => new DoorRepository(connectionString));
builder.Services.AddScoped<IDoorService, DoorService>();


// Add MVC services
builder.Services.AddControllersWithViews();

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

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
