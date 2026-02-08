using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class ReprogramarFechaDeClaseCommandHandler : ICommandHandler<ReprogramarFechaDeClaseCommand, Result>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICursoRepository _curseRepository;
    
    public ReprogramarFechaDeClaseCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository)
    {
        _unitOfWork = unitOfWork;
        _curseRepository = cursoRepository;
    }
    
    public async Task<Result> Handle(ReprogramarFechaDeClaseCommand request, CancellationToken cancellationToken)
    {
        Curso curso = await this._curseRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        
        Clase clase = curso.ObtenerClase(request.IdClase);
        
        clase.ReprogramarClase(request.NuevaFecha);
        
        await this._unitOfWork.SaveChangesAsync();
        return Result.Success(); 
    }
}