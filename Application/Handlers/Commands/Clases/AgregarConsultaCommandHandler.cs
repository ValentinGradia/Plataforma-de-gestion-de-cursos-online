using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Exceptions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Application.Exceptions.Clases;

internal class AgregarConsultaCommandHandler : ICommandHandler<AgregarConsultaCommand,Result>
{
    private readonly ICursoRepository _cursoRepository;
    private readonly IEstudianteRepository _estudianteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AgregarConsultaCommandHandler(ICursoRepository cursoRepository, IEstudianteRepository estudianteRepository, IUnitOfWork unitOfWork)
    {
        this._cursoRepository = cursoRepository;
        this._estudianteRepository = estudianteRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AgregarConsultaCommand request, CancellationToken cancellationToken)
    {
        Task<Estudiante> TaskEstudiante = this._estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        Task<Curso> TaskCurso = this._cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);

        Estudiante user = await TaskEstudiante;
        Curso curso = await TaskCurso;

        if (user is null || curso is null)
        {
            return Result.Failure(new NotFoundException());
        }

        Clase clase = curso.ObtenerClase(request.IdClase);

        try
        {
            curso.ValidarSiElEstudianteNoPerteneceAlCurso(user.Id);

            clase.AgregarConsulta(request.Titulo,request.Descripcion, user.Id);

            Consulta consultaCreada = clase._consultas.Last();
            await this._cursoRepository.InsertarConsultaAsync(consultaCreada, cancellationToken);

            await this._unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
        catch (EstudianteNoPerteneceAlCurso e)
        {
            return Result.Failure(e);
        }

    }
}