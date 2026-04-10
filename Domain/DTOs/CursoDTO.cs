using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.DTOs;

public class CursoDTO
{
    public Guid IdCurso { get; set; }
    public Guid IdProfesor { get; set; }
    public EstadoCurso Estado { get; set; }
    public DateRange Duracion { get; set; }
    public string Nombre { get; set; }
    public string Temario { get; set; }
}