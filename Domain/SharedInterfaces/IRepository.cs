namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

//Los repositorios no tienen logica de negocio, solo se encargan de la persistencia de los objetos
public interface IRepository<T>
{
    Task GuardarAsync(T objeto);
    Task ActualizarAsync(T clase, CancellationToken cancellationToken);
    
    Task<T?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<T>> ObtenerTodosAsync();
}