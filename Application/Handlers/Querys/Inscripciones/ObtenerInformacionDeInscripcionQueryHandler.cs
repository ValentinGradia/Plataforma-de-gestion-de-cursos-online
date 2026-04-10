using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Inscripciones;

internal class ObtenerInformacionDeInscripcionQueryHandler(ICursoRepository cursoRepository, IMapper mapper) : IQueryHandler<ObtenerInformacionDeInscripcionQuery, Result>
{
    public async Task<Result> Handle(ObtenerInformacionDeInscripcionQuery request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));

        try
        {
            Inscripcion inscripcion = curso.ObtenerInscripcionPorId(request.IdInscripcion);
            return new Result(true, mapper.Map<InscripcionDTO>(inscripcion));
        }
        catch (Exception ex)
        {
            return Result.Failure(new ArgumentException("No se encontró la inscripción con el ID proporcionado.", ex));
        }
    }
}
