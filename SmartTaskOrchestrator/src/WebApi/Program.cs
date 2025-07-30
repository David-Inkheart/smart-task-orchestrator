using Application.Interfaces;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<ITaskService, InMemoryTaskService>();

// Usual boilerplate
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1 Documentation", new() { Title = "Smart Task Orchestrator API", Version = "v1" });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();


app.MapControllers();


app.MapGet("/", () => "Hello World!");


app.Run();

