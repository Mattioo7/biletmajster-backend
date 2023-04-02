
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
using Microsoft.EntityFrameworkCore.Query;

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
//services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("LocalApi"), builder =>
//{
//    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
//}));

services.AddScoped<ICategoriesRepository, CategoriesRepository>();
services.AddScoped<IModelEventRepository, ModelEventRepository>();
services.AddScoped<IPlaceRepository,PlaceRepository>();

// Data Base Section:


builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreated();
//}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
