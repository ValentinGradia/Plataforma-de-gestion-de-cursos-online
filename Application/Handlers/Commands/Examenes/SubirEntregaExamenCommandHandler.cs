using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Examenes;

internal class SubirEntregaExamenCommandHandler : ICommandHandler<SubirEntregaExamenCommand, Guid>
{
    public SubirEntregaExamenCommandHandler(IUnitOfWork unitOfWork, ICursoRepository cursoRepository, IEstudianteRepository estudianteRepository)
    {
        _unitOfWork = unitOfWork;
        this._cursoRepository = cursoRepository;
        this._estudianteRepository = estudianteRepository;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICursoRepository _cursoRepository;
    private readonly IEstudianteRepository _estudianteRepository;
    
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
            request.Respuesta);

        Task<Estudiante> tareaEstudiante = this._estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        Curso curso = await this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        Estudiante estudiante = await tareaEstudiante;

        Inscripcion inscripcion = curso.ObtenerInscripcionPorEstudiante(estudiante.Id);

        inscripcion.AgregarEntregaAlHistorial(entrega);

        await this._cursoRepository.InsertarEntregaExamenAsync(entrega, request.IdExamen, cancellationToken);
        await this._unitOfWork.SaveChangesAsync();

        return entrega.Id;
    }
}