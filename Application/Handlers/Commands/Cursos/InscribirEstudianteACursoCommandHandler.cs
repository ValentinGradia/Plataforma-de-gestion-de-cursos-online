using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class InscribirEstudianteACursoCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository) : ICommandHandler<InscribirEstudianteACursoCommand, Result>
{
    public async Task<Result> Handle(InscribirEstudianteACursoCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        
        Inscripcion inscripcion = Inscripcion.CrearInscripcion(request.IdEstudiante, request.IdCurso);
        curso.AgregarEstudiante(inscripcion);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
    
}