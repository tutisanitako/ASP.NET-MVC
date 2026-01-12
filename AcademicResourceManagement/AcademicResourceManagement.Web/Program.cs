using AcademicResourceManagement.Application;
using AcademicResourceManagement.Infrastructure;
using AcademicResourceManagement.Persistence;
using AcademicResourceManagement.Persistence.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container using Dependency Injection
builder.Services.AddControllersWithViews();

// Register layers with Dependency Injection Pattern
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

var app = builder.Build();

// Seed database with roles and users
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.InitializeAsync(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();