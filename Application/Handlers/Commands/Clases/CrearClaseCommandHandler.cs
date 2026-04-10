using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Clases;

internal class CrearClaseCommandHandler : ICommandHandler<CrearClaseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICursoRepository _cursoRepository;

    public CrearClaseCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository)
    {
        this._unitOfWork = unitOfWork;
        this._cursoRepository = cursoRepository;
    }
    
    public async Task<Guid> Handle(CrearClaseCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        Guid idClase = curso.IniciarClase(request.Material);

        Clase claseCreada = curso.ObtenerClase(idClase);
        await this._cursoRepository.InsertarClaseAsync(claseCreada, cancellationToken);

        await this._unitOfWork.SaveChangesAsync();
        return idClase;
    }
}