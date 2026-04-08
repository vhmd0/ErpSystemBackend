var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

builder.AddProject<Projects.API>("api")
    .WithReference(redis);

builder.Build().Run();
