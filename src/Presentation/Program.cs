using Presentation.Configurations;
using Serilog;
using Infrastructure;
using Presentation.Configuration;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersionings();
builder.Services.AddSwaggerDocumentation(configuration: builder.Configuration);
builder.Services.AddDenpendencies(builder.Configuration);
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));

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