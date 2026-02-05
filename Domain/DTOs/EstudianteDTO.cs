using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.DTOs;


//Relacion por agregacion para solo utilizar los datos del usuario sin obligatoriamente
//exponer la entidad completa de Usuario dentro de EstudianteDTO.
public class EstudianteDTO
{
    public UsuarioDTO Usuario { get; protected set; }
    public List<Curso?> CursosInscritosActualmente { get; set; }
    public List<Curso?> HistorialDeCursos { get; set; }
}