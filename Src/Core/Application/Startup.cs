using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Prototype.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        /*
        
        return services
            .AddValidatorsFromAssembly(assembly)
            .AddMediatR(assembly);
        */
        return services;

    }
}
