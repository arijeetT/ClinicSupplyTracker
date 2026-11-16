using ClinicSupplyTracker.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Keep the hosted database outside the deployment folder so updates preserve it.
var azureHome = Environment.GetEnvironmentVariable("HOME");
var isAzureAppService = !string.IsNullOrEmpty(
    Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));
var databasePath = "clinic.db";
if (isAzureAppService && !string.IsNullOrEmpty(azureHome))
{
    var dataDirectory = Path.Combine(azureHome, "data", "ClinicSupplyTracker");
    Directory.CreateDirectory(dataDirectory);
    databasePath = Path.Combine(dataDirectory, "clinic.db");
}

builder.Services.AddDbContext<ClinicContext>(options =>
    options.UseSqlite(new SqliteConnectionStringBuilder
    {
        DataSource = databasePath
    }.ToString()));
    
var app = builder.Build();

// This single-instance demo applies its checked-in schema when Azure starts it.
if (isAzureAppService)
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<ClinicContext>().Database.Migrate();
}

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
