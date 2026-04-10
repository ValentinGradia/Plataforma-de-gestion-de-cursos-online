using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class ActualizarTemarioCommandHandler(ICursoRepository cursoRepository, IUnitOfWork _unitOfWork) : ICommandHandler<ActualizarTemarioCommand, Result>
{
    public async Task<Result> Handle(ActualizarTemarioCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        
        curso!.ActualizarTemario(request.Temario);

        await cursoRepository.ActualizarAsync(curso, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
