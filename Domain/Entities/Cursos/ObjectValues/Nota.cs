using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

//va a ser un record porque las notas de los examenes son inmutables una vez asignadas.
//Luego, no deberia contener el curso o un estudiante -> porque la nota la maneja el curso
// es algo que DEPENDE del curso, no algo independiente. Y por eso solo va a estar dentro del 
//aggregate de curso
public record Nota
{
    public Guid IdCurso { get; init; }
    public Guid EstudianteId { get; init; }
    public decimal ValorNota { get; init; } 

    private Nota(Guid idCurso, Guid estudianteId, decimal valorNota)
    {
        this.IdCurso = idCurso;
        this.EstudianteId = estudianteId;
        this.ValorNota = valorNota;
    }

    public static Nota AsignarNota(Guid idCurso, Guid estudianteId, decimal valorNota)
    {
        //validaciones de negocio
        if (valorNota < 0 || valorNota > 10)
            throw new ArgumentOutOfRangeException(nameof(valorNota), "La nota debe estar entre 0 y 10.");

        return new Nota(idCurso, estudianteId, valorNota);
    }
}