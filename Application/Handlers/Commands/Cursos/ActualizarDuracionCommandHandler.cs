using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Application.Exceptions;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class ActualizarDuracionCommandHandler(ICursoRepository cursoRepository, IUnitOfWork _unitOfWork) : ICommandHandler<ActualizarDuracionCommand, Result>
{
    public async Task<Result> Handle(ActualizarDuracionCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        curso!.ActualizarDuracion(request.NuevaDuracion);

        await cursoRepository.ActualizarAsync(curso, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
