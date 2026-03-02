using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Examenes;

internal class ObtenerDetalleExamenQueryHandler(ICursoRepository cursoRepository) : IQueryHandler<ObtenerDetalleExamenQuery, Result>
{
    public async Task<Result> Handle(ObtenerDetalleExamenQuery request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));

        try
        {
            Examen examen = curso.ObtenerModeloExamen(request.IdExamen);
            return new Result(true, examen);
        }
        catch (Exception ex)
        {
            return Result.Failure(ex);
        }
    }
}
