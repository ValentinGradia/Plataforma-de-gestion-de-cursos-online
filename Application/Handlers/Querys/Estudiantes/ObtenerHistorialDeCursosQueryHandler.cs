using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Estudiantes;

internal class ObtenerHistorialDeCursosQueryHandler(IEstudianteRepository estudianteRepository)
    : IQueryHandler<ObtenerHistorialDeCursos, Result>
{
    public async Task<Result> Handle(ObtenerHistorialDeCursos request, CancellationToken cancellationToken)
    {
        IEnumerable<Guid> historial = await estudianteRepository.ObtenerHistorialCursosDeEstudianteAsync(
            request.IdEstudiante,
            cancellationToken
        );

        return new Result(true, historial);
    }
}

