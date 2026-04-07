using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaDeGestionDeCursosOnline.Application.Abstractions.Messaging;
using PlataformaDeGestionDeCursosOnline.Application.Querys.Inscripciones;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

namespace PlataformaDeGestionDeCursosOnline.Presentation.Controllers.Inscripciones;

[ApiController]
[Route("api/inscripciones")]
public class InscripcionController(ISender sender) : ControllerBase
{
    // Queries
    [HttpGet("{idInscripcion}/curso/{idCurso}")]
    public async Task<IActionResult> GetInformacionDeInscripcion([FromRoute] Guid idCurso ,[FromRoute] Guid idInscripcion)
    {
        IQuery<Result> query = new ObtenerInformacionDeInscripcionQuery(idInscripcion, idCurso);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("{idInscripcion}/entregas")]
    public async Task<IActionResult> GetEntregasDeCursada([FromRoute] Guid idCurso ,[FromRoute] Guid idInscripcion)
    {
        IQuery<Result> query = new ObtenerEntregasDeCursadaQuery(idInscripcion, idCurso);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }

    [HttpGet("{idInscripcion}/asistencia")]
    public async Task<IActionResult> GetPorcentajeDeAsistenciaPorCurso([FromRoute] Guid idCurso ,[FromRoute] Guid idInscripcion)
    {
        IQuery<Result> query = new ObtenerPorcentajeDeAsistenciaPorCursoQuery(idInscripcion, idCurso);
        var result = await sender.Send(query);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
    }
}

