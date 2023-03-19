
//Data base:
using biletmajster_backend.Database;
using biletmajster_backend.Database.Entities;
using biletmajster_backend.Database.Repositories;
using biletmajster_backend.Database.Repositories.Interfaces;


using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

//Odkomentowaæ i zmieniæ ten usesql

//services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultLocal")));
//services.AddIdentity<ApplicationUser, IdentityRole>(config =>
//{
//    config.SignIn.RequireConfirmedEmail = false;
//}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
//services.AddScoped<IApplicationUserRepositories, ApplicationUserRepositories>();

// Data Base Section:


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
