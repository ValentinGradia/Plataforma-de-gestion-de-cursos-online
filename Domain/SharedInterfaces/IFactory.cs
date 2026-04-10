namespace PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

public interface IFactory<T>
{
    T Crear();
}