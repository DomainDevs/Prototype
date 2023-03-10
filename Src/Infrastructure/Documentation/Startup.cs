using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Prototype.Infrastructure.Documentation
{
    internal static class Startup
    {
        internal static IServiceCollection AddOpenApiDocumentation(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            if (settings.Enable)
            {
                services.AddSwaggerGen(config =>
                    {
                        //version
                        var Contact = new OpenApiContact()
                        {
                            Name = settings.Contact,
                            Email = settings.ContactEmail
                        };

                        config.SwaggerDoc(settings.Version1, new OpenApiInfo
                        {
                            Version = settings.Version1,
                            Title = settings.Title + " " + settings.Version1,
                            Description = settings.Description,
                            Contact = Contact

                        });

                        config.SwaggerDoc(settings.Version2, new OpenApiInfo
                        {
                            Version = settings.Version2,
                            Title = settings.Title + " " + settings.Version2,
                            Description = settings.Description,
                            Contact = Contact
                        });
                        
                        //JWT Security
                        config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "JWT Authentication",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                        });

                        config.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[]{}
                            }
                        });
                    }
                );
            }

            return services;
        }

        internal static IApplicationBuilder UseOpenApiDocumentation(this IApplicationBuilder app, IConfiguration config)
        {
            if (config.GetValue<bool>("SwaggerSettings:Enable"))
            {
                app.UseDeveloperExceptionPage()
                .UseSwagger()
                .UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "RealStateSite v1");
                    config.SwaggerEndpoint("/swagger/v2/swagger.json", "RealStateSite v2");
                }
                );
            }

            return app;
        }
    }
}
