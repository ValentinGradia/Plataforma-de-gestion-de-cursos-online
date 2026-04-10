using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos.Clases;

public record ActualizarMaterialCommand(Guid IdCurso,Guid IdClase, string NuevoMaterial) : ICommand<Result>;
