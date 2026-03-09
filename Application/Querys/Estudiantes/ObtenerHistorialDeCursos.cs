using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Estudiantes;

public record ObtenerHistorialDeCursos(Guid IdEstudiante) : IQuery<Result>;