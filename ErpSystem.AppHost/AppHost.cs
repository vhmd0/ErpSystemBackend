using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var postgres = builder.AddPostgres("DataBase");

builder.AddProject<API>("api")
    .WithReference(redis)
    .WithReference(postgres);


builder.Build().Run();