using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class CrearClaseCommandHandler : ICommandHandler<CrearClaseCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICursoRepository _cursoRepository;

    public CrearClaseCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository)
    {
        this._unitOfWork = unitOfWork;
        this._cursoRepository = cursoRepository;
    }
    
    public Task<Guid> Handle(CrearClaseCommand request, CancellationToken cancellationToken)
    {
        Clase clase = Clase.CrearClase(
            request.IdCurso,
            request.Material
        );
        
        Curso curso = this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken).Result;
        curso.AgregarClase(clase);
        
        this._unitOfWork.SaveChangesAsync();
        return Task.FromResult(clase.Id);
    }
}