using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Cursos;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Cursos;

[ApiController]
[Route("api/cursos")]
public class CursoController(ISender sender) : ControllerBase
{
    
    //Querys
    [HttpGet("{idCurso}")]
    public async Task<IActionResult> GetInformacionCursoPorId([FromRoute] Guid idCurso)
    {
        IQuery<Result> query = new ObtenerInformacionCursoPorIdQuery(idCurso);
        var result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("{idCurso}/clases")]
    public async Task<IActionResult> GetClasesPorCurso([FromRoute] Guid idCurso)
    {
        IQuery<Result> query = new ObtenerClasesPorCursoQuery(idCurso);
        var result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("{idCurso}/estudiantes-inscriptos")]
    public async Task<IActionResult> GetEstudiantesInscriptos([FromRoute] Guid idCurso)
    {
        IQuery<Result> query = new ObtenerEstudiantesInscriptosCursoQuery(idCurso);
        var result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    //Commands
    [HttpPost]
    public async Task<IActionResult> CrearCurso([FromBody] CrearCursoCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdProfesor == Guid.Empty)
            return BadRequest("IdProfesor inválido.");

        if (string.IsNullOrWhiteSpace(command.temario) || string.IsNullOrWhiteSpace(command.nombreCurso))
            return BadRequest("Temario o nombreCurso inválidos.");

        var result = await sender.Send(command);

        return result == Guid.Empty ? NotFound() : Ok(result);
    }

    [HttpPatch("duracion")]
    public async Task<IActionResult> ActualizarDuracion([FromBody] ActualizarDuracionCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty)
            return BadRequest("IdCurso inválido.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPatch("temario")]
    public async Task<IActionResult> ActualizarTemario([FromBody] ActualizarTemarioCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty)
            return BadRequest("IdCurso inválido.");

        if (string.IsNullOrWhiteSpace(command.Temario))
            return BadRequest("Temario inválido.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPatch("profesor")]
    public async Task<IActionResult> CambiarProfesor([FromBody] CambiarProfesorCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdProfesor == Guid.Empty)
            return BadRequest("IdCurso o IdProfesor inválido.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost("inscribir-estudiante")]
    public async Task<IActionResult> InscribirEstudiante([FromBody] InscribirEstudianteACursoCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdEstudiante == Guid.Empty)
            return BadRequest("IdCurso o IdEstudiante inválido.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }
    

    [HttpPost("desinscribir-estudiante")]
    public async Task<IActionResult> DesinscribirEstudiante([FromBody] DesinscribirEstudiante? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdEstudiante == Guid.Empty)
            return BadRequest("IdCurso o IdEstudiante inválido.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost("finalizar")]
    public async Task<IActionResult> FinalizarCurso([FromBody] FinalizarCursoCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty)
            return BadRequest("IdCurso inválido.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }
}