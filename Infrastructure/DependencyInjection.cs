using Microsoft.EntityFrameworkCore;
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
        
        //Cadena de conexion para inicializar los servicios de Entity framework Y Dapper, se obtiene del appsettings.json
        var connectionString = configuration.GetConnectionString("Database") ?? throw new ArgumentNullException(nameof(configuration));

        // Migracion de la base de datos, se registra el contexto de la base de datos con la cadena de conexion y se especifica que se va a usar postgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            //Sirve para ejecutar los procesos de migraciones
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention(); // En postgreSQL, es común usar snake_case para los nombres de tablas y columnas
        });

        return services;
    }
}