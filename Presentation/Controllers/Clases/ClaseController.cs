using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Commands.Clases;
using PlataformaDeGestionDeCursosOnline.Application.Cursos.InscribirEstudianteACurso;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Clases;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Clases;

[ApiController]
[Route("api/clases")]
public class ClaseController(ISender sender) : ControllerBase
{
    
    //QUERYS
    [HttpGet("curso/{idCurso}")]
    public async Task<IActionResult> GetClasesPorCurso([FromRoute] Guid idCurso)
    {
        IQuery<Result> query = new ObtenerClasesPorCursoQuery(idCurso);
        var result = await sender.Send(query);
        
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }
    
    [HttpGet("{idClase}")]
    public async Task<IActionResult> GetInformacionClase([FromRoute] Guid idClase)
    {
        IQuery<Result> query = new ObtenerInformacionClaseQuery(idClase);
        var result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("consultas/{idClase}")]
    public async Task<IActionResult> GetConsultasDeClase([FromRoute] Guid idClase)
    {
        IQuery<Result> query = new ObtenerConsultasDeClaseQuery(idClase);
        var result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("asistencias/{idClase}")]
    public async Task<IActionResult> GetAsistenciasDeClase([FromRoute] Guid idClase)
    {
        IQuery<Result> query = new ObtenerAsistenciasDeClase(idClase);
        var result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }
    
    //COMMANDS
    [HttpPost]
    public async Task<IActionResult> CrearClase([FromBody] Guid idCurso, [FromBody] string material)
    {
        ICommand<Guid> command = new CrearClaseCommand(idCurso, material);

        var result = await sender.Send(command);

        return result == Guid.Empty ? NotFound() : Ok(result);
    }

    [HttpPatch("material")]
    public async Task<IActionResult> ActualizarMaterial([FromBody] ActualizarMaterialCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdClase == Guid.Empty)
            return BadRequest("IdCurso o IdClase inválido.");

        if (string.IsNullOrWhiteSpace(command.NuevoMaterial))
            return BadRequest("NuevoMaterial inválido.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost("consultas")]
    public async Task<IActionResult> AgregarConsulta([FromBody] AgregarConsultaCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdEstudiante == Guid.Empty || command.IdClase == Guid.Empty)
            return BadRequest("Ids inválidos.");

        if (string.IsNullOrWhiteSpace(command.Titulo) || string.IsNullOrWhiteSpace(command.Descripcion))
            return BadRequest("Titulo o Descripcion inválidos.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost("presente")]
    public async Task<IActionResult> DarPresente([FromBody] DarPresenteCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdClase == Guid.Empty || command.IdInscripcionEstudiante == Guid.Empty)
            return BadRequest("Ids inválidos.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost("finalizar")]
    public async Task<IActionResult> FinalizarClase([FromBody] FinalizarClaseCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdClase == Guid.Empty)
            return BadRequest("Ids inválidos.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }

    [HttpPost("reprogramar-fecha")]
    public async Task<IActionResult> ReprogramarFecha([FromBody] ReprogramarFechaDeClaseCommand? command)
    {
        if (command is null)
            return BadRequest("Request body is required.");

        if (command.IdCurso == Guid.Empty || command.IdClase == Guid.Empty)
            return BadRequest("Ids inválidos.");

        if (command.NuevaFecha == default)
            return BadRequest("NuevaFecha inválida.");

        var result = await sender.Send(command);

        return result.IsSuccess
            ? (result.Data is null ? Ok("La operación fue exitosa.") : Ok(result.Data))
            : BadRequest(result.ErrorMessage);
    }
    
}