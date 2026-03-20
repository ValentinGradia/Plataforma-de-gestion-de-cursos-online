using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Email;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Infrastructure.Data;
using PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure;

public static class DependencyInjection
{
    //Aca vamos a registrar los servicios de infraestructura, como repositorios, servicios de correo, etc.
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        
        services.AddScoped<IEmailService, EmailService>();
        
        services.AddScoped<ICursoRepository, CursoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEncuestasRepository, EncuestasRepository>();
        services.AddScoped<IProfesorRepository, ProfesorRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        return services;
    }
}