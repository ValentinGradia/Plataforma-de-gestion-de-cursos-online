using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Profesores;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Profesores;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Profesores;

[ApiController]
[Route("api/profesores")]
public class ProfesorController(ISender sender) : ControllerBase
{
    // Queries
    [HttpGet("{idProfesor}")]
    public async Task<IActionResult> GetPerfilProfesor([FromRoute] Guid idProfesor)
    {
        IQuery<Result> query = new ObtenerPerfilProfesorPorIdQuery(idProfesor);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    // Commands
    [HttpPost]
    public async Task<IActionResult> CrearProfesor([FromBody] CrearProfesorCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");
        
        var result = await sender.Send(command);
        return result == Guid.Empty ? BadRequest() : Ok(result);
    }
}

