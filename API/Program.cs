using API.Extensions;
using API.Middleware;
using ErpSystem.ServiceDefaults;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddRedisInfrastructure();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

var redisConnection = builder.Configuration.GetConnectionString("redis");
var redis = !string.IsNullOrEmpty(redisConnection) 
    ? ConnectionMultiplexer.Connect(redisConnection) 
    : null;
builder.Services.AddInfrastructure(builder.Configuration, redis);

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();