using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Encuestas;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Encuestas;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Encuestas;

[ApiController]
[Route("api/encuestas")]
public class EncuestaController(ISender sender) : ControllerBase
{
    // Queries
    [HttpGet("{idEncuesta}")]
    public async Task<IActionResult> GetEncuestaPorId([FromRoute] Guid idEncuesta)
    {
        IQuery<Result> query = new ObtenerEncuestaPorIdQuery(idEncuesta);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("curso/{idCurso}")]
    public async Task<IActionResult> GetEncuestasPorCurso([FromRoute] Guid idCurso)
    {
        IQuery<Result> query = new ObtenerEncuestasPorCursoQuery(idCurso);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("curso/{idCurso}/mejores-resenas")]
    public async Task<IActionResult> GetMejoresResenasPorCurso([FromRoute] Guid idCurso, [FromQuery] int cantidad)
    {
        IQuery<Result> query = new ObtenerMejoresReseñasPorCursoQuery(idCurso, cantidad);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("curso/{idCurso}/peores-resenas")]
    public async Task<IActionResult> GetPeoresResenasPorCurso([FromRoute] Guid idCurso, [FromQuery] int cantidad)
    {
        IQuery<Result> query = new ObtenerPeoresReseñasPorCursoQuery(idCurso, cantidad);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    // Commands
    [HttpPost]
    public async Task<IActionResult> CrearEncuesta([FromBody] CrearEncuestaCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");
        
        var result = await sender.Send(command);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpPatch]
    public async Task<IActionResult> ModificarEncuesta([FromBody] ModificarEncuestaCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");

        var result = await sender.Send(command);
        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }
}
