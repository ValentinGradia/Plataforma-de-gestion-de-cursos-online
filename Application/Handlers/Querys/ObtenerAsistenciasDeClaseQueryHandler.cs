using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys;

internal class ObtenerAsistenciasDeClaseQueryHandler(ICursoRepository cursoRepository) : IQueryHandler<ObtenerAsistenciasDeClase, Result>
{
    public async Task<Result> Handle(ObtenerAsistenciasDeClase request, CancellationToken cancellationToken)
    {
        Clase? clase = await cursoRepository.ObtenerClasePorId(request.IdClase, cancellationToken);

        if (clase is null)
        {
            return Result.Failure(new ArgumentException("No se encontró la clase con el ID proporcionado."));
        }

        return new Result(true, clase.Asistencias);
    }
}