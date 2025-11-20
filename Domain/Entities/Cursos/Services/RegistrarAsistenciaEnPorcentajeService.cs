using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public class RegistrarAsistenciaEnPorcentajeService
{
    public double CalcularPorcentajeAsistencia(int totalClases, int clasesAsistidas)
    {
        return (double)clasesAsistidas / totalClases * 100;
    }
    
    public void ActualizarPorcentajeAsistencia(Inscripcion inscripcion, int totalClases, int clasesAsistidas)
    {
        if (inscripcion == null)
            throw new ArgumentNullException(nameof(inscripcion));
        
        double nuevoPorcentaje = CalcularPorcentajeAsistencia(totalClases, clasesAsistidas);
        inscripcion.porcentajeAsistencia = nuevoPorcentaje;
    }

    public void RegistrarAsistenciaDeEstudiantes(List<Clase> clases)
    {
        
    }
    
}