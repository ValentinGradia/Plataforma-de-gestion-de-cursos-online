using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys;

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