using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Clases;

public record AgregarConsultaCommand(Guid IdCurso ,Guid IdEstudiante, Guid IdClase, string Titulo, string Descripcion) : ICommand<Result>;
