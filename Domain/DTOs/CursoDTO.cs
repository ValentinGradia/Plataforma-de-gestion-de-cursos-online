using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

namespace PlataformaDeGestionDeCursosOnline.Application.DTOs;

public class CursoDTO
{
    public Guid IdCurso { get; set; }
    public Profesor Profesor { get; set; }
    public EstadoCurso Estado { get; set; }
    public DateRange Duracion { get; set; }
    public string Nombre { get; set; }
    public string Temario { get; set; }
}