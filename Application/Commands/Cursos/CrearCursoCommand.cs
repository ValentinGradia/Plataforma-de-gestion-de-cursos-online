using MediatR;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Profesores;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record CrearCursoCommand(Guid IdProfesor, string temario, string nombreCurso, DateTime inicio, DateTime fin) 
    : ICommand<Guid>;