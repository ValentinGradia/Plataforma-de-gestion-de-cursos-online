using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.DTOs;

//Relacion por agregacion para solo utilizar los datos del usuario sin obligatoriamente
//exponer la entidad completa de Usuario dentro de ProfesorDTO.
public class ProfesorDTO
{
    public UsuarioDTO Usuario { get; protected set; }
    public List<Curso> CursosACargo { get; set; }
    public string Especialidad { get; set; }
    
}