using PlataformaDeGestionDeCursosOnline.Domain.Entities;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IClaseRepository : IRepository<Clase>
{
    void ActualizarMaterial(Guid idClase, string nuevoMaterial);
    
    void AgregarConsulta(Guid idUsuario, Guid idClase, string titulo, string descripcion);
    
    bool EstudiantePerteneceAClase(Guid idUsuario, Guid idClase);
}