using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Examenes;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Examenes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Examenes;

[ApiController]
[Route("api/examenes")]
public class ExamenController(ISender sender) : ControllerBase
{
    // Queries
    [HttpGet("{idExamen}/curso/{idCurso}")]
    public async Task<IActionResult> GetDetalleExamen([FromRoute] Guid idCurso,[FromRoute] Guid idExamen)
    {
        IQuery<Result> query = new ObtenerDetalleExamenQuery(idCurso, idExamen);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("entregas/{idEntrega}")]
    public async Task<IActionResult> GetDetalleEntregaExamen([FromRoute] Guid idCurso,[FromRoute] Guid idExamen)
    {
        IQuery<Result> query = new ObtenerDetalleEntregaExamenQuery(idCurso, idExamen);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("entregas/{idEntrega}/nota")]
    public async Task<IActionResult> GetNotaDeEntregaExamen([FromRoute] Guid idCurso,[FromRoute] Guid idExamen)
    {
        IQuery<Result> query = new ObtenerNotaDeEntregaExamenQuery(idCurso, idExamen);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    // Commands
    [HttpPost("modelo")]
    public async Task<IActionResult> SubirModeloExamen([FromBody] SubirModeloExamenCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");
        
        var result = await sender.Send(command);
        return result == Guid.Empty ? BadRequest() : Ok(result);
    }

    [HttpPost("entrega")]
    public async Task<IActionResult> SubirEntregaExamen([FromBody] SubirEntregaExamenCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");
        
        var result = await sender.Send(command);
        return result == Guid.Empty ? BadRequest() : Ok(result);
    }

    [HttpPost("entregas/nota")]
    public async Task<IActionResult> PonerNotaEntregaExamen([FromBody] PonerNotaEntregaExamenCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");

        var result = await sender.Send(command);
        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }
}

