using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;

namespace PlataformaDeGestionDeCursosOnline.Application.Querys.Estudiantes;

public record ObtenerPerfilEstudianteQuery(Guid IdEstudiante) : IQuery<EstudianteDTO>;