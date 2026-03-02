using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Encuestas;

internal class ObtenerEncuestaPorIdQueryHandler(IEncuestasRepository encuestasRepository) : IQueryHandler<ObtenerEncuestaPorIdQuery, Result>
{
    public async Task<Result> Handle(ObtenerEncuestaPorIdQuery request, CancellationToken cancellationToken)
    {
        Encuesta? encuesta = await encuestasRepository.ObtenerPorIdAsync(request.IdEncuesta, cancellationToken);
        if (encuesta is null)
            return Result.Failure(new ArgumentException("No se encontró la encuesta con el ID proporcionado."));
        return new Result(true, encuesta);
    }
}
