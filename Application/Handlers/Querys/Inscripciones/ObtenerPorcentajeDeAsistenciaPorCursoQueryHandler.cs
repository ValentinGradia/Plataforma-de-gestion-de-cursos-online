using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.SharedInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Inscripciones;

internal class ObtenerPorcentajeDeAsistenciaPorCursoQueryHandler(ICursoRepository cursoRepository)
    : IQueryHandler<ObtenerPorcentajeDeAsistenciaPorCursoQuery, Result>
{
    public async Task<Result> Handle(ObtenerPorcentajeDeAsistenciaPorCursoQuery request,
        CancellationToken cancellationToken)
    {
        Curso curso = await cursoRepository.ObtenerPorIdAsync(request.IdCurso, cancellationToken);
        if (curso is null)
            return Result.Failure(new ArgumentException("No se encontró el curso con el ID proporcionado."));

        try
        {
            Inscripcion inscripcion = curso.ObtenerInscripcionPorId(request.IdInscripcion);
            double porcentajeAsistencia = inscripcion.porcentajeAsistencia;
            return new Result(true, porcentajeAsistencia);
        }
        catch (Exception e)
        {
            return Result.Failure(e);
        }
    }
}