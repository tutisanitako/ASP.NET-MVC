using Homework3.Application.Interfaces;
using Homework3.Application.Services;
using Homework3.Domain.Interfaces;
using Homework3.Infrastructure.Data;
using Homework3.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Services and Repositories
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

// Add CORS for the Web project
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWeb", policy =>
    {
        // Add the Web project's port (7059) as an allowed origin
        policy.WithOrigins("https://localhost:7059", "http://localhost:5059") // Always add both HTTPS and HTTP, just in case
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowWeb");

app.UseAuthorization();

app.MapControllers();

app.Run();