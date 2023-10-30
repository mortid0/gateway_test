using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 1000;
        options.Window = TimeSpan.FromSeconds(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();
app.UseRateLimiter();
app.MapReverseProxy();

static string GetTicks() => (DateTime.Now.Ticks & 0x11111).ToString("00000");

app.MapGet("/hello", () => Results.Ok($"Hello {GetTicks()}"))
                           .RequireRateLimiting("fixed");

app.Run();