using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class CrearCursoCommandHandler(ICursoRepository cursoRepository, IProfesorRepository profesorRepository, IUnitOfWork _unitOfWork) : ICommandHandler<CrearCursoCommand, Guid>
{
    public async Task<Guid> Handle(CrearCursoCommand request, CancellationToken cancellationToken)
    {
        Curso curso = Curso.CrearCurso(request.IdProfesor, request.temario, request.nombreCurso, request.inicio, request.fin);
        Task<Profesor>? taskProfesor =  profesorRepository.ObtenerPorIdAsync(curso.IdProfesor, cancellationToken);
        
        await cursoRepository.GuardarAsync(curso);
        Profesor profesor = await taskProfesor;
        profesor.AgregarCursoACargo(curso.Id);
        await _unitOfWork.SaveChangesAsync();

        return curso.Id;
    }
}

