using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Profesores;

internal class ObtenerPerfilProfesorPorIdQueryHandler(IProfesorRepository profesorRepository, IMapper mapper) : IQueryHandler<ObtenerPerfilProfesorPorIdQuery, Result>
{
    public async Task<Result> Handle(ObtenerPerfilProfesorPorIdQuery request, CancellationToken cancellationToken)
    {
        Profesor profesor = await profesorRepository.ObtenerPorIdAsync(request.IdProfesor, cancellationToken);
        if (profesor is null)
            return Result.Failure(new ArgumentException("No se encontró el profesor con el ID proporcionado."));

        return new Result(true, mapper.Map<ProfesorDTO>(profesor));
    }
}
