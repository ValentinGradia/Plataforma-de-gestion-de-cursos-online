using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record IniciarClaseCommand(Guid IdCurso, string MaterialClase) : ICommand, IRequest<Guid>;