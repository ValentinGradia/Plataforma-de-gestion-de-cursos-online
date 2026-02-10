using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class FinalizarClaseCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository) : ICommandHandler<FinalizarClaseCommand,Result>
{
    public async Task<Result> Handle(FinalizarClaseCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        curso.FinalizarClase(request.IdClase);
        
        //El curso donde estan todos los alumnos inscriptos
        List<Guid> inscriptosTotales = curso.Inscripciones.Select(i => i.Id).ToList(); 
        
        //La clase donde estan los alumnos presentes
        Clase clase = curso.ObtenerClase(request.IdClase);
        List<Guid> idInscriptosPresentes = clase.Asistencias.Select(a => a.IdInscripcionEstudiante).ToList();
        
        //Devuelve los elementos del primer conjunto que no están en el segundo conjunto, utilizando el método Except de LINQ
        List<Guid> idInscriptosAusentes = inscriptosTotales.Except(idInscriptosPresentes).ToList();
        
        //Los estudiantes que no marcaron presente, se les marca ausente automaticamente cuando finaliza la clase
        foreach (Guid idAusente in idInscriptosAusentes)
        {
            //obtenemos la inscripcion del estudiante ausente para agregarle la asistencia de ausente
            Inscripcion inscripcion = curso.ObtenerInscripcionPorId(idAusente);
            Asistencia asistenciaAusenteDeEstudiante = clase.DarAusente(idAusente);
            inscripcion.AgregarAsistencia(asistenciaAusenteDeEstudiante);
        }
        
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}