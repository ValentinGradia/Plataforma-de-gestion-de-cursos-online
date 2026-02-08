using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class CambiarProfesorCommandHandler(ICursoRepository cursoRepository, IProfesorRepository profesorRepository, IUnitOfWork _unitOfWork) : ICommandHandler<CambiarProfesorCommand, Result>
{
    public async Task<Result> Handle(CambiarProfesorCommand request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        Profesor? profesor = await profesorRepository.ObtenerPorIdAsync(request.IdProfesor, cancellationToken);
        
        curso.CambiarProfesor(profesor);

        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

