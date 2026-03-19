using System.Threading.RateLimiting;
using FreshX.API.Middleware;
using FreshX.Application;
using FreshX.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataProtection();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Add rate limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        // Only limit auth endpoints
        var path = context.Request.Path.Value?.ToLower();
        if (path != null && (path.Contains("/api/account/login") ||
                             path.Contains("/api/account/register") ||
                             path.Contains("/api/account/forgot-password") ||
                             path.Contains("/api/account/verify-reset-otp") ||
                             path.Contains("/api/account/reset-password") ||
                             path.Contains("/api/account/refreshtoken")))
        {
            // Limit by IP: 5 requests per minute
            return RateLimitPartition.GetFixedWindowLimiter(
                context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                partition => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 2
                });
        }
        // No limit for other endpoints
        return RateLimitPartition.GetNoLimiter("nolimit");
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseRateLimiter();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.Run();
