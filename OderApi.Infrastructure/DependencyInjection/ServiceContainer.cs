using eCommerce.SharedLibrary.DependancyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OderApi.Application.Interface;
using OderApi.Infrastructure.Data;
using OderApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OderApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceProvider AddInfrastrucureService(this IServiceCollection services, IConfiguration config)
        {
            // Add Database Connectivity
            // Add authenincation schema

            SharedServiceContainer.AddSharedServices<OrderDbContext>(services, config, config["MySerilog:FileName"]!);

            // Create Dependancy Injection
            services.AddScoped<IOrder, OrderRepository>();

            return services.BuildServiceProvider();
        }

        public static IApplicationBuilder UserInfrastrucureService(this IApplicationBuilder app)
        {
            // Register middleware such as:
            // Global Exception -> handle external errors
            // ListenToApiGateway only -> block all outsider calls

            SharedServiceContainer.UseSharedPolicies(app);

            return app;
        }
    }
}
