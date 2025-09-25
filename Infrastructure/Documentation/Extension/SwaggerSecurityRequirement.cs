using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Documentation.Extension;

//Extender los métodos swagger
public class SwaggerSecurityRequirement : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var controllerActionDescriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

        if (controllerActionDescriptor == null)
            return;

        // 🔍 Buscar AllowAnonymous manualmente
        var attributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(true);
        bool allowAnonymousOnMethod = Array.Exists(attributes, attr => attr is AllowAnonymousAttribute);


        bool allowAnonymousOnController = Array.Exists(
            controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes(true),
            attr => attr is AllowAnonymousAttribute
        );

        if (allowAnonymousOnMethod || allowAnonymousOnController)
        {
            // ❌ No agregar seguridad al método, si es anónimo (AllowAnonymous)
            return;
        }

        // ✅ Agregar seguridad si no es anónimo (AllowAnonymous)
        OpenApiSecurityScheme scheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };
        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new OpenApiSecurityRequirement
            {
                [scheme] = Array.Empty<string>()
            }
        };


    }
}
