using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class AgregarConsultaCommandHandler : ICommandHandler<AgregarConsultaCommand,Result>
{
    private readonly IClaseRepository _claseRepository;
    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AgregarConsultaCommandHandler(IClaseRepository claseRepository, IEstudianteRepository estudianteRepository, IUnitOfWork unitOfWork)
    {
        this._claseRepository = claseRepository;
        this._estudianteRepository = estudianteRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AgregarConsultaCommand request, CancellationToken cancellationToken)
    {
        Task<Estudiante> TaskEstudiante = this._estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        Task<Clase> TaskClase = this._claseRepository.ObtenerPorIdAsync(request.IdClase, cancellationToken);

        Usuario user = await TaskEstudiante;
        Clase clase = await TaskClase;

        if (user is null || clase is null)
        {
            return Result.Failure(new NotFoundException());
        }

        if (!(this._claseRepository.EstudiantePerteneceAClase(user.Id, clase.Id)))
        {
            return Result.Failure(new EstudianteNoPerteneceAlCurso());
        }
        
        Consulta consulta = clase.AgregarConsulta(request.Titulo,request.Descripcion, user.Id);

        await this._unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}