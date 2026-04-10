using AutoMapper;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Cursos;

internal class ObtenerInformacionCursoPorIdQueryHandler(ICursoRepository cursoRepository, IMapper mapper) : IQueryHandler<ObtenerInformacionCursoPorIdQuery, Result>
{
    public async Task<Result> Handle(ObtenerInformacionCursoPorIdQuery request, CancellationToken cancellationToken)
    {
        Curso? curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        
        if (curso is null)
        {
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));
        }
        
        CursoDTO cursoDTO = mapper.Map<CursoDTO>(curso);
        return new Result(true, cursoDTO);
    }
}