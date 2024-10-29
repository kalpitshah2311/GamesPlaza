using Games.API.Data;
using Games.API.Data.GamesRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextFactory<CosmosDbContext>(optionsBuilder =>
  optionsBuilder
    .UseCosmos(
      connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
      databaseName: "ApplicationDB",
      cosmosOptionsAction: options =>
      {
          options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
          options.MaxRequestsPerTcpConnection(16);
          options.MaxTcpConnectionsPerEndpoint(32);
      }));

builder.Services.AddScoped<IGamesRepository, GamesRepository>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CosmosDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

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
