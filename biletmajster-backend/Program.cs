
//Data base:

using System.Text;
using biletmajster_backend.Database;
using biletmajster_backend.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using biletmajster_backend.Configurations;
using biletmajster_backend.Database.Interfaces;
using biletmajster_backend.Interfaces;
using biletmajster_backend.Jwt;
using biletmajster_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"],
            ValidIssuer = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token"]))
        };

        // for mapping custom sessionToken to Bearer token
        options.Events = JwtAuthEventsHandler.Instance;
    });

services.AddLogging();

// Singletons
builder.Services.AddSingleton(
    builder.Configuration.GetRequiredSection("Mail").Get<MailConfiguration>()!
);

// Scoped
builder.Services.AddScoped<ICustomMailService, MailService>();

services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("biletmajster-backend")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// services.AddDbContext<ApplicationDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("LocalApi"), builder =>
// {
//     builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
// }));

services.AddScoped<ICategoriesRepository, CategoriesRepository>();
services.AddScoped<IModelEventRepository, ModelEventRepository>();
services.AddScoped<IAccountConfirmationCodeRepository, AccountConfirmationCodeRepository>();
services.AddScoped<IOrganizersRepository, OrganizersRepository>();
services.AddScoped<IConfirmationService, ConfirmationService>();
services.AddScoped<IOrganizerIdentityManager, OrganizerIdentityManager>();
services.AddScoped<IPlaceRepository, PlaceRepository>();
services.AddScoped<IPlaceRepository,PlaceRepository>();
services.AddScoped<IReservationRepository,ReservationRepository>();
services.AddScoped<IReservationService,ReservationService>();

// Data Base Section:


builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
    {
        policy.SetIsOriginAllowed(host => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    }));

builder.Services.AddSwaggerGenNewtonsoftSupport();

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
