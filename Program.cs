using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 2000;
        options.Window = TimeSpan.FromSeconds(1);
    }));

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "debuglimit", options =>
    {
        options.PermitLimit = 1;
        options.Window = TimeSpan.FromSeconds(1);
    }));

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();