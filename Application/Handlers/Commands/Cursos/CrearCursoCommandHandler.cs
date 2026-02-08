using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class CrearCursoCommandHandler(ICursoRepository cursoRepository, IProfesorRepository profesorRepository, IUnitOfWork _unitOfWork) : ICommandHandler<CrearCursoCommand, Guid>
{
    public async Task<Guid> Handle(CrearCursoCommand request, CancellationToken cancellationToken)
    {
        Profesor? profesor = await profesorRepository.ObtenerPorIdAsync(request.IdProfesor, cancellationToken);
        
        Curso curso = Curso.CrearCurso(profesor, request.temario, request.nombreCurso, request.inicio, request.fin);

        await cursoRepository.GuardarAsync(curso);
        await _unitOfWork.SaveChangesAsync();

        return curso.Id;
    }
}

