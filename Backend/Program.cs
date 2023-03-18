var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<CoreDbContext>(
//     opts => opts.UseNpgsql(builder.Configuration["DatabaseConnectionString"])
// );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"Config: {builder.Configuration["HduhcikTescik"]}");
Console.WriteLine($"Config: {builder.Configuration["HduhcikPytul"]}");

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
