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
👉 **Remember to click 'Authorize' and enter your SessionId.**  
🔗 GitHub SSO: https://github.com/login/oauth/authorize?client_id={configuration[ConfigKeys.Authorization.GithubSSO.ClientId]}&redirect_uri={configuration[ConfigKeys.Authorization.GithubSSO.CallbackUrl]}&scope=read:user%20user:email&state=Jx8DmnKqJZvNE-
"
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
