using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Encuestas;

public class ModificarEncuestaCommandHandler : ICommandHandler<ModificarEncuestaCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEncuestasRepository _encuestasRepository;
    
    public ModificarEncuestaCommandHandler(IUnitOfWork unitOfWork, IEncuestasRepository encuestasRepository)
    {
        this._unitOfWork = unitOfWork;
        this._encuestasRepository = encuestasRepository;
    }
    
    public async Task<Result> Handle(ModificarEncuestaCommand request, CancellationToken cancellationToken)
    {
        Encuesta encuesta = await this._encuestasRepository.ObtenerPorIdAsync(request.IdEncuesta);
        
        if(encuesta.IdEstudiante != request.IdEstudiante)
        {
            return Result.Failure(new Exception("La encuesta no pertenece al estudiante especificado."));
        }
        
        encuesta.ModificarComentarios(request.Comentarios);
        encuesta.ModificarCalificacionCurso(request.CalificacionCurso);
        encuesta.ModificarCalificacionDocente(request.CalificacionDocente);
        encuesta.ModificarCalificacionMaterial(request.CalificacionMaterial);

        this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}