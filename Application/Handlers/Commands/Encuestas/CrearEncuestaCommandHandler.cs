using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Encuestas;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Commands.Encuestas;

internal class CrearEncuestaCommandHandler : ICommandHandler<CrearEncuestaCommand,Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEncuestasRepository _encuestasRepository;
    private readonly ICursoRepository _cursoRepository;

    public CrearEncuestaCommandHandler(IUnitOfWork unitOfWork, IEncuestasRepository encuestasRepository, ICursoRepository cursoRepository)
    {
        this._unitOfWork = unitOfWork;
        this._cursoRepository = cursoRepository;
        this._encuestasRepository = encuestasRepository;
    }
    
    public async Task<Result> Handle(CrearEncuestaCommand request, CancellationToken cancellationToken)
    {
        Curso ? curso = await this._cursoRepository.ObtenerPorIdAsync(request.idCurso, cancellationToken);
        
        Encuesta encuesta = Encuesta.Crear(
            request.idCurso,
            request.idEstudiante,
            request.calificacionCurso,
            request.calificacionMaterial,
            request.calificacionDocente,
            request.comentarios
        );

        curso.AgregarEncuesta(encuesta);
        //Aca EF no trackea la entidad por lo que tenemos que guardarla explicitamente
        await this._encuestasRepository.GuardarAsync(encuesta);
        await this._unitOfWork.SaveChangesAsync();
        return new Result(true,encuesta.Id);
    }
}