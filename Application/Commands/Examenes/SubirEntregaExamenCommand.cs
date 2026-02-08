using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;

//Agregamos el ID del curso porque nosotros vamos a operar directamente sobre el curso
//no sobre la inscripcion directamente. Entonces, para subir la entrega del examen, necesitamos
//el ID del curso para poder obtener el curso y agregar la entrega del examen a la inscripcion del
//estudiante dentro del curso.
public record SubirEntregaExamenCommand(Guid IdExamen, Guid IdCurso, Guid IdEstudiante,
    string Respuesta, TipoExamen Tipo, DateTime FechaLimite) : ICommand<Guid>;