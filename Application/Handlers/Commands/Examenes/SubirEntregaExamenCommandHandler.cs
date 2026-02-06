using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class SubirEntregaExamenCommandHandler : ICommandHandler<SubirEntregaExamenCommand, Guid>
{
    public SubirEntregaExamenCommandHandler(IUnitOfWork unitOfWork, IInscripcionRepository inscripcionRepository, IEntregasExamenes entregasExamenes)
    {
        _unitOfWork = unitOfWork;
        _inscripcionRepository = inscripcionRepository;
        this._entregasExamenes = entregasExamenes;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IInscripcionRepository _inscripcionRepository;
    private readonly IEntregasExamenes _entregasExamenes;
    
    //No creamos la entrega del examen aparte, sino que lo hacemos dentro del acto de entregar el examen. Cuando 
    //vamos a entregar el examen, lo creamos automaticamente.
    //De esta manera, no creamos la entrega y la subimos en nuestra inscripcion. Sino que directamente al subir
    //la entrega, se crea la entrega y se agrega a la inscripcion del estudiante.
    public async Task<Guid> Handle(SubirEntregaExamenCommand request, CancellationToken cancellationToken)
    {
        EntregaDelExamen entrega = EntregaDelExamen.Crear(
            request.IdExamen,
            request.IdEstudiante,
            request.Tipo,
            request.Respuesta,
            request.FechaLimite);
        
        Task tarea = this._entregasExamenes.GuardarAsync(entrega);
        
        Inscripcion inscripcion = await this._inscripcionRepository.ObtenerPorIdAsync(entrega.IdInscripcionEstudiante);
        
        inscripcion.AgregarEntregaAlHistorial(entrega);
        
        await tarea;
        
        await this._unitOfWork.SaveChangesAsync();
        
        return entrega.Id;
    }
}