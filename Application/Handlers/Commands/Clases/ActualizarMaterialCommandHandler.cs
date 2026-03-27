using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class ActualizarMaterialCommandHandler : ICommandHandler<ActualizarMaterialCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICursoRepository _cursoRepository;
    
    public ActualizarMaterialCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository)
    {
        _unitOfWork = unitOfWork;
        _cursoRepository = cursoRepository;
    }
    
    public async Task<Result> Handle(ActualizarMaterialCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        Clase clase = curso.ObtenerClase(request.IdClase);

        clase.ActualizarMaterial(request.NuevoMaterial);

        await this._cursoRepository.ActualizarClaseAsync(clase, cancellationToken);
        await this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}