using Application.Interfaces;
using Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog FIRST, before anything else
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Optional: validate scope lifetimes (good for dev sanity)
builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = true;
    options.ValidateOnBuild = true;
});

// Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IAIService, FakeAIService>();
builder.Services.AddScoped<ITaskService, InMemoryTaskService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Smart Task Orchestrator API",
        Version = "v1"
    });
});

// Optional but useful: HTTP logging (only logs if Serilog is configured to pick it up)
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

// Middleware order matters!
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); // <-- Adds nice structured request logs from Serilog

app.UseHttpLogging();           // <-- Built-in HTTP logging (for request/response headers)

app.UseAuthorization();

app.MapGet("/", () => "Welcome to Smart Task Orchestrator, v1");

app.MapControllers();

app.Run();
