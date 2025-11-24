using System;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;

public class Encuesta : Entity
{
    public Guid IdCurso { get; private set; }
    public Guid? IdEstudiante { get; private set; }
    public Calificacion? CalificacionCurso { get; private set; }
    public Calificacion? CalificacionMaterial { get; private set; }
    public Calificacion? CalificacionDocente { get; private set; }
    public string Comentarios { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    private Encuesta(
        Guid cursoId,
        Guid? idEstudiante,
        Calificacion calCurso,
        Calificacion calMaterial,
        Calificacion calDocente,
        string comentarios = null) : base(Guid.NewGuid())
    {
        IdCurso = cursoId;
        IdEstudiante = idEstudiante;
        this.CalificacionCurso = calCurso;
        this.CalificacionMaterial = calMaterial;
        this.CalificacionDocente = calDocente;
        Comentarios = comentarios;
        FechaCreacion = DateTime.UtcNow;
    }
    
    internal static Encuesta Crear(
        Guid idCurso,
        Guid? idEstudiante,
        int calificacionCurso,
        int calificacionMaterial,
        int calificacionDocente,
        string comentarios)
    {
        // Se delega la validación de rangos al value object Calificacion
        Calificacion calCurso = new Calificacion(calificacionCurso);
        Calificacion calMaterial = new Calificacion(calificacionMaterial);
        Calificacion calDocente = new Calificacion(calificacionDocente);
        
        return new Encuesta(
            idCurso,
            idEstudiante,
            calCurso,
            calMaterial,
            calDocente,
            comentarios
        );
    }
    
    
    
}