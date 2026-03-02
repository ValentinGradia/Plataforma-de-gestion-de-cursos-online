using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Estudiantes;

internal class ObtenerPerfilEstudiantePorIdQueryHandler(IEstudianteRepository estudianteRepository, IMapper mapper) : IQueryHandler<ObtenerPerfilEstudiantePorIdQuery, Result>
{
    public async Task<Result> Handle(ObtenerPerfilEstudiantePorIdQuery request, CancellationToken cancellationToken)
    {
        Estudiante estudiante = await estudianteRepository.ObtenerPorIdAsync(request.IdEstudiante, cancellationToken);
        if (estudiante is null)
            return Result.Failure(new ArgumentException("No se encontró el estudiante con el ID proporcionado."));

        return new Result(true, mapper.Map<EstudianteDTO>(estudiante));
    }
}
