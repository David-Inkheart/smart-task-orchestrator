using Application.Interfaces;
using Infrastructure.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// 🔥 Add config layering
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// 🔥 Add Serilog early
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Host.UseDefaultServiceProvider(options =>
{
    options.ValidateScopes = builder.Environment.IsDevelopment();
    options.ValidateOnBuild = builder.Environment.IsDevelopment();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// 🧠 Register services
// builder.Services.AddScoped<IAIService, FakeAIService>();
// builder.Services.AddScoped<ITaskService, InMemoryTaskService>();

builder.Services.AddSingleton<IAIService, FakeAIService>();
builder.Services.AddSingleton<ITaskService, InMemoryTaskService>();


// 🧪 Swagger (only in Dev or config-controlled)
bool enableSwagger = builder.Configuration.GetValue<bool>("EnableSwagger");
if (enableSwagger || builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new() { Title = "Smart Task Orchestrator API", Version = "v1" });
    });
}

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

if (enableSwagger || app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();
// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => $"Welcome to Smart Task Orchestrator - {app.Environment.EnvironmentName}");

app.Run();
