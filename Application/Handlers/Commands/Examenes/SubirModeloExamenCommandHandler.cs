using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class SubirModeloExamenCommandHandler : ICommandHandler<SubirModeloExamenCommand, Result>
{
    public SubirModeloExamenCommandHandler(IUnitOfWork unitOfWork, IExamenRepository examenes, ICursoRepository cursos)
    {
        _unitOfWork = unitOfWork;
        _examenes = examenes;
        _cursos = cursos;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IExamenRepository _examenes;
    private readonly ICursoRepository _cursos;
    
    public async Task<Result> Handle(SubirModeloExamenCommand request, CancellationToken cancellationToken)
    {
        Examen examen = await this._examenes.ObtenerPorIdAsync(request.idExamen);
        
        Curso curso = await this._cursos.ObtenerPorIdAsync(examen.IdCurso);

        curso.CargarExamen(examen);

        this._unitOfWork.SaveChangesAsync();
        return Result.Success();

    }
}