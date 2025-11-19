namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IRepository<T>
{
    Task GuardarAsync(T objeto);
    
    Task<T?> ObtenerPorIdAsync(Guid id);
    
    Task<IEnumerable<T>> ObtenerTodosAsync();
}