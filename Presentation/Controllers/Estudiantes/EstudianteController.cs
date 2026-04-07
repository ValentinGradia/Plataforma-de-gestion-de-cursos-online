using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Estudiantes;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Estudiantes;

[ApiController]
[Route("api/estudiantes")]
public class EstudianteController(ISender sender) : ControllerBase
{
    // Queries
    [HttpGet("{idEstudiante}")]
    public async Task<IActionResult> GetPerfilEstudiante([FromRoute] Guid idEstudiante)
    {
        IQuery<Result> query = new ObtenerPerfilEstudiantePorIdQuery(idEstudiante);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("{idEstudiante}/cursos-activos")]
    public async Task<IActionResult> GetCursosInscriptosActualmente([FromRoute] Guid idEstudiante)
    {
        IQuery<Result> query = new ObtenerCursosInscriptosActualmente(idEstudiante);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("{idEstudiante}/historial-cursos")]
    public async Task<IActionResult> GetHistorialDeCursos([FromRoute] Guid idEstudiante)
    {
        IQuery<Result> query = new ObtenerCursosInscriptosActualmente(idEstudiante);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    // Commands
    [HttpPost]
    public async Task<IActionResult> CrearEstudiante([FromBody] CrearEstudianteCommand? command)
    {
        if (command is null) return BadRequest("Request body is required.");
        
        var result = await sender.Send(command);
        return result == Guid.Empty ? BadRequest() : Ok(result);
    }
}

