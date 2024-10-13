var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.AspireApp2_ApiService>("apiservice", null)
    .WithHttpEndpoint(port: 11444, targetPort: 11444, isProxied: false)
    .WithHttpsEndpoint(port: 11445, targetPort: 11445, isProxied: false);

builder.AddProject<Projects.AspireApp2_Web>("webfrontend", null)
    .WithExternalHttpEndpoints()
    .WithHttpEndpoint(port: 11434, targetPort: 11434, isProxied: false)
    .WithHttpsEndpoint(port: 11435, targetPort: 11435, isProxied: false)
    .WithReference(cache)
    .WithReference(apiService);

builder.Build().Run();
