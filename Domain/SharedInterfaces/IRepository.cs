namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IRepository<T>
{
    Task GuardarAsync(T objeto);
    
    Task<T?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<T>> ObtenerTodosAsync();
}