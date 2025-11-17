using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

public class Estudiante : Entity 
{
    private List<Curso> cursosInscritosActualmente = new();
    private List<Curso> historialDeCursos = new();
    private Guid IdUsuario { get; init; }
    
    public Estudiante(Guid id) : base(id)
    {
    }
}