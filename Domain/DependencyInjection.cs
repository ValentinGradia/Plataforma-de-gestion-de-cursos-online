using Microsoft.Extensions.DependencyInjection;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using Microsoft.Extensions.DependencyInjection;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Interfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Services;

namespace PlataformaDeGestionDeCursosOnline.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IInscripcionService, InscripcionService>();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        return services;
    }
}

