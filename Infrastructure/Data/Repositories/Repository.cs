using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext DbContext;
    
    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }
}