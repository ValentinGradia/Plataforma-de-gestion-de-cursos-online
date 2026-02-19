using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Application.Handlers.Querys.Cursos;

internal class ObtenerEstudiantesInscriptosCursoQueryHandler(ICursoRepository cursoRepository): IQueryHandler<ObtenerEstudiantesInscriptosCursoQuery, Result>
{
    public async Task<Result> Handle(ObtenerEstudiantesInscriptosCursoQuery request, CancellationToken cancellationToken)
    {
        List<Estudiante>? estudiantes = await cursoRepository.ObtenerEstudiantesInscriptosEnCurso(request.IdCurso, cancellationToken);
        
        if(estudiantes is null)
            return Result.Failure(new ArgumentException("No se encontro el curso con el ID proporcionado."));
        
        return new Result(true,estudiantes);
    }
    
}