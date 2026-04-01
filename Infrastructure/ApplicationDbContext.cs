using Microsoft.EntityFrameworkCore;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure;

//Agregamos nuestra clase principal de Db Context, que hereda de DbContext y de IUnitOfWork para manejar las transacciones
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    //OPtion pertenece a la cadena conexion, y se inyecta a través del constructor
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    // Aplicamos las configuraciones de nuestras entidades utilizando el método ApplyConfigurationsFromAssembly,
    // que busca todas las clases que implementan IEntityTypeConfiguration en el ensamblado actual y las aplica automáticamente.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    // public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     await base.SaveChangesAsync(cancellationToken);
    // }
    
}