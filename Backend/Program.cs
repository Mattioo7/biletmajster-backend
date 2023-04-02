
//Data base:
using biletmajster_backend.Database;
using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories;
using biletmajster_backend.Database.Repositories.Interfaces;


using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Backend.Configurations;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

// builder.Services.AddDbContext<CoreDbContext>(
//     opts => opts.UseNpgsql(builder.Configuration["DatabaseConnectionString"])
// );

// Singletons
builder.Services.AddSingleton(
    builder.Configuration.GetRequiredSection("Mail").Get<MailConfiguration>()!
);

// Scoped
builder.Services.AddScoped<MailService>();

services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b=> b.MigrationsAssembly("biletmajster-backend")));

services.AddScoped<ICategoriesRepository, CategoriesRepository>();

// Data Base Section:


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(host => host.StartsWith(builder.Configuration["FrontendUrl"]!))
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
