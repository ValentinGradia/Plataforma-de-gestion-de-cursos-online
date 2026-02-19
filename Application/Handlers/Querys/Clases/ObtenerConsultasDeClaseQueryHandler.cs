using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys;

internal class ObtenerConsultasDeClaseQueryHandler(ICursoRepository cursoRepository) : IQueryHandler<ObtenerConsultasDeClaseQuery, Result>
{

    public async Task<Result> Handle(ObtenerConsultasDeClaseQuery request, CancellationToken cancellationToken)
    {
        Clase? clase = await cursoRepository.ObtenerClasePorId(request.IdClase, cancellationToken);

        if (clase is null)
        {
            return Result.Failure(new ArgumentException("No se encontró la clase con el ID proporcionado."));
        }

        return new Result(true, clase._consultas);
    }
}