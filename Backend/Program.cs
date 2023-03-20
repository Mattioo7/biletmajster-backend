using Backend.Configurations;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<CoreDbContext>(
//     opts => opts.UseNpgsql(builder.Configuration["DatabaseConnectionString"])
// );

// Singletons
builder.Services.AddSingleton(
    builder.Configuration.GetRequiredSection("Mail").Get<MailConfiguration>()!
);

// Scoped
builder.Services.AddScoped<MailService>();

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
