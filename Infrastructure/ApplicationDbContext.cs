using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure;

//Agregamos nuestra clase principal de Db Context, que hereda de DbContext y de IUnitOfWork para manejar las transacciones
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    
    //OPtion pertenece a la cadena conexion, y se inyecta a través del constructor
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    // Aplicamos las configuraciones de nuestras entidades utilizando el método ApplyConfigurationsFromAssembly,
    // que busca todas las clases que implementan IEntityTypeConfiguration en el ensamblado actual y las aplica automáticamente.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    //Sobre escribimos el metodo porque DBcontext ya lo implementa
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken); //Confirmamos todas las transacciones que estan en memorio
        
        await PublishDomainEventsAsync(); //Publicamos los eventos de dominio que se hayan generado durante la transacción
        
        return result;
    }
    
    private async Task PublishDomainEventsAsync()
    {
        //Obtenemos todas las entidades que tienen eventos de dominio pendientes
        var domainEntities = ChangeTracker.Entries<Entity>()
            .Select(e => e.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

        //Publicamos cada evento de dominio utilizando MediatR
        foreach (var domainEvent in domainEntities)
        {
            await _publisher.Publish(domainEvent);
        }
    }
    
}