using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Examenes;

internal class ObtenerDetalleEntregaExamenQueryHandler(ICursoRepository cursoRepository) : IQueryHandler<ObtenerDetalleEntregaExamenQuery, Result>
{
    public async Task<Result> Handle(ObtenerDetalleEntregaExamenQuery request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));
        try
        {
            // Intentamos usar el método del curso que busca entrega por id
            EntregaDelExamen entrega = curso.ObtenerEntregaDeExamen(request.IdEntregaExamen);
            return new Result(true, entrega);
        }
        catch (Exception ex)
        {
            return Result.Failure(new ArgumentException("No se encontró la entrega de examen con el ID proporcionado.", ex));
        }
    }
}
