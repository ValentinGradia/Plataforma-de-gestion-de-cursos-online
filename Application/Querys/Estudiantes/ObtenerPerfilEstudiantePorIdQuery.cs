using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Estudiantes;

public record ObtenerPerfilEstudiantePorIdQuery(Guid IdEstudiante) : IQuery<Result>;
