using MediatR;
using Microsoft.EntityFrameworkCore;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure;

//Agregamos nuestra clase principal de Db Context, que hereda de DbContext y de IUnitOfWork para manejar las transacciones
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher? _publisher;
    
    //OPtion pertenece a la cadena conexion, y se inyecta a través del constructor
    
    
    //Creamos otro constructor solo para EF
    //Resultado
    //Run time -> Usa el constructor con IPublisher
    //Design time / Migraciones-> Usa el constructor sin IPublisher, porque en ese momento no tenemos acceso
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    // Aplicamos las configuraciones de nuestras entidades utilizando el método ApplyConfigurationsFromAssembly,
    // que busca todas las clases que implementan IEntityTypeConfiguration en el ensamblado actual y las aplica automáticamente.
    //En nuestro caso iria a buscar los archivos de configuration de cada entidad.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    //Sobre escribimos el metodo porque DBcontext ya lo implementa
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await base.SaveChangesAsync(cancellationToken); //Confirmamos todas las transacciones que estan en memoria
            //Aca el EF core se encarga de inspeccionar el Change Tracker (rastreador de cambios) para detectar todas las entidades que han sido modificadas,
            //agregadas o eliminadas durante la transacción.
            
            await PublishDomainEventsAsync(); //Publicamos los eventos de dominio que se hayan generado durante la transacción
            
            return result;
        }
        // Este ex de dispara cuando hay un problema de violacion de reglas de la BBDD 
        catch (DbUpdateConcurrencyException e)
        {
            throw new ConcurrencyException("La excepcion por concurrencia se disparó",e);
        }
    }
    
    private async Task PublishDomainEventsAsync()
    {
        if(_publisher == null) return; //Si el publisher es null, no hacemos nada,
        //esto es para evitar problemas en el diseño de migraciones, porque en ese momento no tenemos acceso al publisher
        
        
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