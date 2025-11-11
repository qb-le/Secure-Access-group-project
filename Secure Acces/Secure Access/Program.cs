using DAL;
using DAL.Interfaces;
using DAL.repository;
using Logic.Interface;
using Logic.Service;
using Logic.Services;
using Microsoft.EntityFrameworkCore;
using Secure_Access.Services;
using Logic.Classes;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IAuditLogRepository>(provider  => new  AuditLogRepository(connectionString));
builder.Services.AddScoped<AuditLogService>();
builder.Services.AddScoped<IUserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IDoorRepository>(provider => new DoorRepository(connectionString));
builder.Services.AddScoped<IDoorService, DoorService>();
builder.Services.AddSingleton<QRTokenManager>();
builder.Services.AddScoped<IReceptionService, ReceptionService>();
builder.Services.AddScoped<IReceptionistRepository>(provider => new ReceptionistRepository(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});





// Add MVC services
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

app.MapControllers();
app.MapHub<AccessHub>("/accessHub");
app.UseCors("AllowAll");


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
    pattern: "{controller=Login}/{action=Index}/{id?}");


app.Run();
