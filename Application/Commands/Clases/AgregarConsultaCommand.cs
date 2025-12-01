using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;

public record AgregarConsultaCommand(Guid IdEstudiante, Guid IdClase, string Titulo, string Descripcion) : ICommand;