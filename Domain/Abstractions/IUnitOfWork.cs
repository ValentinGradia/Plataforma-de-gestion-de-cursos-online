namespace PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

//para la confirmación de insercion de datos en la BBDD usamos el patrón Unit of Work.
//Este patrón agrupa una serie de operaciones en una sola transacción
public interface IUnitOfWork
{
    //el cancellation token es para cancelar la operación si es necesario
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}