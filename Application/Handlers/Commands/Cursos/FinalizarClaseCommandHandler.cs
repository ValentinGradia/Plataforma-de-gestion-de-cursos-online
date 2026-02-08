using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class FinalizarClaseCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository) : ICommandHandler<FinalizarClaseCommand,Result>
{
    public async Task<Result> Handle(FinalizarClaseCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        curso.FinalizarClase(request.IdClase);
        
        unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}