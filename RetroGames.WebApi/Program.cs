using MongoDB.Driver;
using RetroGames.Core.Abstractions.Configurations;
using RetroGames.Core.Abstractions.Services;
using RetroGames.Core.Services;
using RetroGames.Data.Abstractions.Repositories;
using RetroGames.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

var mongoConfig = new MongoDbConfigurations();
builder.Configuration.GetSection(nameof(MongoDbConfigurations)).Bind(mongoConfig);
builder.Services.Configure<MongoDbConfigurations>(builder.Configuration.GetSection(nameof(MongoDbConfigurations)));
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(m => new MongoClient(mongoConfig.ConnectionString));

builder.Services.AddTransient<IProviderRepository, ProviderRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IGameRepository, GameRepository>();
builder.Services.AddTransient<ICommentRepository, CommentRepository>();

builder.Services.AddTransient<IProviderService, ProviderService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IGamesService, GamesService>();
builder.Services.AddTransient<ICommentsService, CommentsService>();

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = true);

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
