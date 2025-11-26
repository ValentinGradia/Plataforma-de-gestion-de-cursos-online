using Microsoft.Extensions.DependencyInjection;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

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
        services.AddTransient<InscripcionService>();
        return services;
    }
}