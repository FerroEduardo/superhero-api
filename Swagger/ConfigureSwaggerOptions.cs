using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using System.ComponentModel;

namespace SuperHeroAPI.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        public static readonly string TokenDefinitionName = "token";
        public void Configure(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(TokenDefinitionName, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please provide a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference
            //            {
            //                Type = ReferenceType.SecurityScheme,
            //                Id = "token"
            //            }
            //        },
            //        Array.Empty<string>()
            //    }
            //});
            options.OperationFilter<SecureEndpointAuthRequirementFilter>();
        }
    }
}
