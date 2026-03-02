using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Encuestas;

internal class ObtenerEncuestasPorCursoQueryHandler(IEncuestasRepository encuestasRepository) : IQueryHandler<ObtenerEncuestasPorCursoQuery, Result>
{
    public async Task<Result> Handle(ObtenerEncuestasPorCursoQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Encuesta> todas = await encuestasRepository.ObtenerTodosAsync();
        List<Encuesta> filtradas = todas.Where(e => e.IdCurso == request.IdCurso).ToList();
        return new Result(true, filtradas);
    }
}
