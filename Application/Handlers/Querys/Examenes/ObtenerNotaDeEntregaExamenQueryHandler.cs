using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos.Notas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Examenes;

internal class ObtenerNotaDeEntregaExamenQueryHandler(ICursoRepository cursoRepository) : IQueryHandler<ObtenerNotaDeEntregaExamenQuery, Result>
{
    public async Task<Result> Handle(ObtenerNotaDeEntregaExamenQuery request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));

        try
        {
            EntregaDelExamen entrega = curso.ObtenerEntregaDeExamen(request.IdEntregaExamen);
            return new Result(true, entrega.Nota);
        }
        catch (Exception ex)
        {
            return Result.Failure(new ArgumentException("No se encontró la nota para la entrega de examen especificada.", ex));
        }
    }
}
