using NewEra.Domain.Interface;
using NewEra.BLL;
using NewEra.Dal;
  

var builder = WebApplication.CreateBuilder(args);

string connectionString = "Server=mssqlstud.fhict.local;Database=dbi578294_newworld;User Id=dbi578294_newworld;Password=newworld;TrustServerCertificate=True;";

builder.Services.AddScoped<IProduct>(_ => new NewEraProducts(connectionString));
builder.Services.AddScoped<NeweraProductService>();

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

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
