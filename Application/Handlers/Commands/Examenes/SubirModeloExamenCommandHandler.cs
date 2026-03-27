using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class SubirModeloExamenCommandHandler : ICommandHandler<SubirModeloExamenCommand, Guid>
{
    public SubirModeloExamenCommandHandler(IUnitOfWork unitOfWork, 
        ICursoRepository cursos)
    {
        _unitOfWork = unitOfWork;
        _cursos = cursos;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICursoRepository _cursos;

    public async Task<Guid> Handle(SubirModeloExamenCommand request, CancellationToken cancellationToken)
    {

        Examen modeloExamen = Examen.CrearExamen(
            request.IdCurso,
            request.Tipo,
            request.TemaExamen,
            request.FechaLimite
        );

        Curso curso = await _cursos.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        curso.CargarExamen(modeloExamen);

        await _cursos.InsertarExamenAsync(modeloExamen, cancellationToken);
        await _unitOfWork.SaveChangesAsync();

        return modeloExamen.Id;

    }
}