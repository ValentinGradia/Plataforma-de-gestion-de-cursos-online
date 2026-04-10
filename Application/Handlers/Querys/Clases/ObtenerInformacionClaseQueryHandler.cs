using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Clases;

internal class ObtenerInformacionClaseQueryHandler(ICursoRepository cursoRepository, IMapper mapper) : IQueryHandler<ObtenerInformacionClaseQuery, Result>
{
    public async Task<Result> Handle(ObtenerInformacionClaseQuery request, CancellationToken cancellationToken)
    {
        Clase? clase = await cursoRepository.ObtenerClasePorId(request.IdClase, cancellationToken);

        if (clase is null)
        {
            return Result.Failure(new ArgumentException("No se encontró la clase con el ID proporcionado."));
        }

        ClaseDTO claseDto = mapper.Map<ClaseDTO>(clase);
        return new Result(true, claseDto);
    }
}