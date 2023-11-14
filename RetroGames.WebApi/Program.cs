using MongoDB.Driver;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Core.Services;
using RetroGames.Data.Abstractions.Repositories;
using RetroGames.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IMongoClient>(m => new MongoClient("mongodb://localhost:27018/"));

builder.Services.AddTransient<IRetrogamesRepository, RetrogamesRepository>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

//builder.Services.AddControllers();
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
