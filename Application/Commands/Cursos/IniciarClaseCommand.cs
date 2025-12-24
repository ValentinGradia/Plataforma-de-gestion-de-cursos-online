using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record IniciarClaseCommand(Guid IdCurso, string MaterialClase) : ICommand, IRequest<Guid>;
//Nos va a devolver el Guid de la clase que fue iniciada

