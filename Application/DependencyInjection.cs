using Microsoft.Extensions.DependencyInjection;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Infrastructure.Data;
using PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

namespace PlataformaDeGestionDeCursosOnline.Application;

//Agregamos el contenedor de dependencias que administre toda la configuracion de
//dependency injection para este proyecto, que despues vamos a referencias apuntando
//hacia un proyecto API -> el cual va a levantar la solucion
public static class DependencyInjection
{
    //registramos la comunicacion entre los commands, querys ,handlers utilizando
    //el patron Mediator ejecutandose sobre el paquete MediatR
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //de esta fora vamos a poder inyectar los servicios que vayamos creando,
        //agrega MediatR a nuestro proyecto (Commands -> Handlers, Querys -> Handlers,
        // Notifications -> Handlers, Events -> Handlers)
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        
        //Aca vamos a registrar todos los servicios de aplicacion
        services.AddScoped<IInscripcionService,InscripcionService>();

        // --- Registramos fábrica de conexiones y repositorios Dapper (Infrastructure) ---
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();

        services.AddScoped<ICursoRepository, CursoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IEncuestasRepository, EncuestasRepository>();

        return services;
    }
}