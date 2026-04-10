using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Inscripciones;

internal class ObtenerEntregasDeCursadaQueryHandler(ICursoRepository cursoRepository) : IQueryHandler<ObtenerEntregasDeCursadaQuery, Result>
{
    public async Task<Result> Handle(ObtenerEntregasDeCursadaQuery request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));

        try
        {
            Inscripcion inscripcion = curso.ObtenerInscripcionPorId(request.IdInscripcion);
            return new Result(true, inscripcion.ObtenerHistorialDeEntregas());
        }
        catch (Exception ex)
        {
            return Result.Failure(ex);
        }
    }
}
