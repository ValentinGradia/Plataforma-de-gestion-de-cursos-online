using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;

//va a ser un record porque las notas de los examenes son inmutables una vez asignadas.
//Luego, no deberia contener el curso o un estudiante -> porque la nota la maneja el curso
// es algo que DEPENDE del curso, no algo independiente. Y por eso solo va a estar dentro del 
//aggregate de curso
public record Nota
{
    private decimal ValorNota { get; set; } 

    public Nota(decimal valorNota)
    {
        this.ValorNota = valorNota;
    }

    public void AsignarNota(decimal valor) => this.ValorNota = valor;
}