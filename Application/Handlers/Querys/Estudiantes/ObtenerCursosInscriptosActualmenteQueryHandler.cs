using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Estudiantes;

internal class ObtenerCursosInscriptosActualmenteQueryHandler(IEstudianteRepository estudianteRepository)
    : IQueryHandler<ObtenerCursosInscriptosActualmente, Result>
{
    public async Task<Result> Handle(ObtenerCursosInscriptosActualmente request, CancellationToken cancellationToken)
    {
        IEnumerable<Guid> cursosActivos = await estudianteRepository.ObtenerCursosActivosDeEstudianteAsync(
            request.IdEstudiante,
            cancellationToken
        );

        return new Result(true, cursosActivos);
    }
}

