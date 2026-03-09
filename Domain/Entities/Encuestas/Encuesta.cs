using System;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;

public class Encuesta : Entity
{
    public Guid IdCurso { get; private set; }
    public Guid? IdEstudiante { get; private set; }
    public Calificacion CalificacionCurso { get; private set; }
    public Calificacion CalificacionMaterial { get; private set; }
    public Calificacion CalificacionDocente { get; private set; }
    public string Comentarios { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private Encuesta(
        Guid cursoId,
        Guid? idEstudiante,
        Calificacion calCurso,
        Calificacion calMaterial,
        Calificacion calDocente,
        string comentarios = null
    ) : base(Guid.NewGuid())
    {
        IdCurso = cursoId;
        IdEstudiante = idEstudiante;
        CalificacionCurso = calCurso;
        CalificacionMaterial = calMaterial;
        CalificacionDocente = calDocente;
        Comentarios = comentarios;
        FechaCreacion = DateTime.UtcNow;
    }

    // Constructor interno para reconstrucción desde BD
    internal Encuesta(Guid id, Guid cursoId, Guid? idEstudiante, Calificacion calCurso, Calificacion calMaterial, Calificacion calDocente, string comentarios, DateTime fechaCreacion) : base(id)
    {
        IdCurso = cursoId;
        IdEstudiante = idEstudiante;
        CalificacionCurso = calCurso;
        CalificacionMaterial = calMaterial;
        CalificacionDocente = calDocente;
        Comentarios = comentarios;
        FechaCreacion = fechaCreacion;
    }

    public static Encuesta Reconstruir(
        Guid id,
        Guid idCurso,
        Guid? idEstudiante,
        int calificacionCurso,
        int calificacionMaterial,
        int calificacionDocente,
        string comentarios,
        DateTime fechaCreacion)
    {
        return new Encuesta(
            id,
            idCurso,
            idEstudiante,
            new Calificacion(calificacionCurso),
            new Calificacion(calificacionMaterial),
            new Calificacion(calificacionDocente),
            comentarios,
            fechaCreacion
        );
    }

    public static Encuesta Crear(
        Guid idCurso,
        Guid? idEstudiante,
        int calificacionCurso,
        int calificacionMaterial,
        int calificacionDocente,
        string comentarios)
    {
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
    
    public void ModificarCalificacionCurso(int nuevaCalificacion)
    {
        CalificacionCurso = new Calificacion(nuevaCalificacion);
    }

    public void ModificarCalificacionMaterial(int nuevaCalificacion)
    {
        CalificacionMaterial = new Calificacion(nuevaCalificacion);
    }

    public void ModificarCalificacionDocente(int nuevaCalificacion)
    {
        CalificacionDocente = new Calificacion(nuevaCalificacion);
    }

    public void ModificarComentarios(string nuevosComentarios)
    {
        Comentarios = nuevosComentarios;
    }
    
}
