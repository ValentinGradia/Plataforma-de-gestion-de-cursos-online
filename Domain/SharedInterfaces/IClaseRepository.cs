using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

public interface IClaseRepository : IRepository<Clase>
{
    
    bool EstudiantePerteneceAClase(Guid idUsuario, Guid idClase);
}