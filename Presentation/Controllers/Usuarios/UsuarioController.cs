using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Usuarios;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Usuarios;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController(ISender sender) : ControllerBase
{
    // Queries
    [HttpGet("{idUsuario}")]
    public async Task<IActionResult> GetPerfilUsuario([FromRoute] Guid idUsuario)
    {
        IQuery<Result> query = new ObtenerPerfilUsuarioQuery(idUsuario);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    // Commands
    [HttpPatch("{idUsuario}/contacto")]
    public async Task<IActionResult> ActualizarContacto([FromRoute] Guid idUsuario, [FromBody] ActualizarContactoCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");

        var result = await sender.Send(command);
        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPatch("{idUsuario}/datos-personales")]
    public async Task<IActionResult> ActualizarDatosPersonales([FromRoute] Guid idUsuario, [FromBody] ActualizarDatosPersonalesCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");

        var result = await sender.Send(command);
        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPatch("{idUsuario}/direccion")]
    public async Task<IActionResult> ActualizarDireccion([FromRoute] Guid idUsuario, [FromBody] ActualizarDireccionCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");

        var result = await sender.Send(command);
        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }
}

