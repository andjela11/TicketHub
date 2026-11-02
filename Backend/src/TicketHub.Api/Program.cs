var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var password = Environment.GetEnvironmentVariable("SA_PASSWORD");
var redisPassword = Environment.GetEnvironmentVariable("REDIS_PASSWORD");
builder.Services.AddHealthChecks()
.AddSqlServer($"Server=localhost,1433;Database=master;User Id=sa;Password=Admin1234!;TrustServerCertificate=True")
.AddRedis($"localhost:6379,password=Admin1234!");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();