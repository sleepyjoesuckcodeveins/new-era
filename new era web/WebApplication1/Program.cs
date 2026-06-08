using NewEra.Domain.Interface;
using NewEra.BLL;
using NewEra.Dal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using NewEra.DAl.repository;


var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=mssqlstud.fhict.local;Database=dbi578294_newworld;User Id=dbi578294_newworld;Password=newworld;TrustServerCertificate=True;";

builder.Services.AddScoped<IProduct>(_ => new NewEraProducts(connectionString));
builder.Services.AddScoped<NeweraProductService>();
builder.Services.AddScoped<LoginService>();

builder.Services.AddScoped<IAdminInterface>(_ => new Admindal(connectionString));
builder.Services.AddScoped<Adminservice>();

// In Program.cs
builder.Services.AddScoped<IUserManagement>(provider => 
    new UserAccess(connectionString));

builder.Services.AddScoped<ICart>(provider => 
    new NewEraProducts(connectionString));
builder.Services.AddScoped<IManageCartProduct, CartSystem>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Login");
    options.AccessDeniedPath = new PathString("/Error");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Events = new CookieAuthenticationEvents();
    options.Cookie.HttpOnly = true;
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSession();

app.UseRouting();
    
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
