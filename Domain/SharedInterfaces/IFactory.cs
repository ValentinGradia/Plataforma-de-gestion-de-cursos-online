namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IFactory<T>
{
    T Crear();
}