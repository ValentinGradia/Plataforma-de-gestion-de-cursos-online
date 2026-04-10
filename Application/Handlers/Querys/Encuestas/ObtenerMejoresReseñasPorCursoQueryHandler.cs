using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Encuestas;

internal class ObtenerMejoresReseñasPorCursoQueryHandler(IEncuestasRepository encuestasRepository) : IQueryHandler<ObtenerMejoresReseñasPorCursoQuery, Result>
{
    public async Task<Result> Handle(ObtenerMejoresReseñasPorCursoQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Encuesta> todas = await encuestasRepository.ObtenerTodosAsync();
        IEnumerable<Encuesta> filtradas = todas.Where(e => e.IdCurso == request.IdCurso);
        List<Encuesta> ordenadas = filtradas.OrderByDescending(e => e.CalificacionCurso.Valor)
                                 .Take(request.CantidadEncuestasAObtener)
                                 .ToList();
        return new Result(true, ordenadas);
    }
}
