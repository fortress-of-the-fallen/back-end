using Presentation.Configurations;
using Serilog;
using Infrastructure;
using Presentation.Configuration;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

var loggerConfig = new LoggerConfiguration()
    .WriteTo.File("Logs/app.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");

if(builder.Environment.IsDevelopment())
{
    loggerConfig.WriteTo.Console(outputTemplate: "[{Timestamp:dd/MM/yyyy HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}");
}

Log.Logger = loggerConfig.CreateLogger();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersionings();
builder.Services.AddSwaggerDocumentation(configuration: builder.Configuration);
builder.Services.AddDenpendencies(builder.Configuration);
builder.Host.UseSerilog();

builder.Services.AddCors(options => options
    .AddDefaultPolicy(policy => policy
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(_ => true)
    ));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fortress of the Fallen API V1");
    c.RoutePrefix = "docs";
});
app.UseCors();

try
{
    var url = $"{Environment.GetEnvironmentVariable("ASPNETCORE_URLS")}/docs";
    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
    {
        FileName = url,
        UseShellExecute = true
    });
}
catch { }

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();