using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Cursos;

internal class ObtenerClasesPorCursoQueryHandler(ICursoRepository cursoRepository, IMapper mapper): IQueryHandler<ObtenerClasesPorCursoQuery,Result>
{
    public async Task<Result> Handle(ObtenerClasesPorCursoQuery request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        
        if(curso is null)
            return Result.Failure(new ArgumentException("No se encontro el curso"));

        IEnumerable<ClaseDTO> clasesDto = curso._clases
            .Select(c => mapper.Map<ClaseDTO>(c));
        
        return new Result(true,clasesDto);
    }
}