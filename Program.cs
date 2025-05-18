using FootballerStatsApi.Data;
using FootballerStatsApi.Repositories;
using FootballerStatsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FootballerStatsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FootballerStatsDbConnection")));

builder.Services.AddScoped<IPlayersRepository, PlayersRepository>();
builder.Services.AddScoped<IMatchStatisticsRepository, MatchStatisticsRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });


builder.Services.AddAutoMapper(typeof(Program));

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
