using System.Reflection;
using Domain.Constains;
using Microsoft.OpenApi.Models;

namespace Presentation.Configuration;

public static class AddSwaggerGen
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
{
    Title = "My API",
    Version = "v1",
    Description = $@"
<p>👉 <strong>Remember to click 'Authorize' and enter your SessionId.</strong></p>
<p>🔗 GitHub SSO: <a href='https://github.com/login/oauth/authorize?client_id={configuration[ConfigKeys.Authorization.GithubSSO.ClientId]}&redirect_uri={configuration[ConfigKeys.Authorization.GithubSSO.CallbackUrl]}&scope=read:user%20user:email&state=Jx8DmnKqJZvNE-'>Click here</a></p>
<p><strong>SignalR Hub:</strong> <code>/hubs/auth</code></p>
<ul>
    <li><code>JoinLoginSessionChannel(string sessionId)</code> – Subscribe to a login session channel.</li>
    <li><code>LeaveLoginSessionChannel(string sessionId)</code> – Unsubscribe from a login session channel.</li>
</ul>"
});

            c.AddSecurityDefinition("SessionId", new OpenApiSecurityScheme
            {
                Description = "Enter your session ID here (no Bearer needed)",
                Name = "X-Session-Id", 
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "SessionId"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = "SessionId" 
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        return services;
    }
}
