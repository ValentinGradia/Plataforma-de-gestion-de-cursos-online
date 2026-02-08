using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Cursos;

internal class FinalizarCursoCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository) : ICommandHandler<FinalizarCursoCommand, Result>
{
    public async Task<Result> Handle(FinalizarCursoCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        curso.FinalizarCurso();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}