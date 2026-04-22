using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;
using PlataformaDeGestionDeCursosOnline.Infrastructure.Data;
using PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure;

public static class DependencyInjection
{
    //Aca vamos a registrar los servicios de infraestructura, como repositorios, servicios de correo, etc.
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddScoped<ICursoRepository, CursoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEncuestasRepository, EncuestasRepository>();
        services.AddScoped<IProfesorRepository, ProfesorRepository>();
        services.AddScoped<IEstudianteRepository, EstudianteRepository>();
        
        
        //Cadena de conexion para inicializar los servicios de Entity framework Y Dapper, se obtiene del appsettings.json
        var connectionString = configuration.GetConnectionString("Database");

        if (string.IsNullOrEmpty(connectionString))
        {
            // Estamos en design-time (EF), no rompemos
            return services;
        }

        // Migracion de la base de datos, se registra el contexto de la base de datos con la cadena de conexion y se especifica que se va a usar postgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            //Sirve para ejecutar los procesos de migraciones
            options.UseNpgsql(connectionString); 
        });
        
        // Registramos el UnitOfWork, que es el contexto de la base de datos, para que se pueda inyectar en los servicios y repositorios que lo necesiten. En nuestro
        // caso es para los domain events.
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}