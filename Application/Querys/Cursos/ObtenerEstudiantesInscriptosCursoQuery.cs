using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;

public record ObtenerEstudiantesInscriptosCursoQuery(Guid IdCurso) : IQuery<List<EstudianteDTO>>;