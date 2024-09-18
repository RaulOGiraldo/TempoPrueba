using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TempoPrueba.Core.Interfaces;
using TempoPrueba.Core.Services;
using TempoPrueba.Intrastructure.Repositories;

namespace TempoPrueba.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
        {
            //-- Dependencias propias - (Inyeccion de dependencias)  --//
            builder.Services.AddTransient<IProveedorRepository, ProveedorRepository>();
            builder.Services.AddTransient<IProveedorService, ProveedorService>();

            builder.Services.AddTransient<IPasswordService, PasswordService>();

            return builder;
        }

    }
}
