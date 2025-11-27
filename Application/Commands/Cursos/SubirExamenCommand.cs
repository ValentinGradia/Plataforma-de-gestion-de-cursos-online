using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;

public record SubirExamenCommand(Guid IdCurso, TipoExamen tipoExamen, string temaExamen, DateTime fechaLimiteDeEntrega) : ICommand;