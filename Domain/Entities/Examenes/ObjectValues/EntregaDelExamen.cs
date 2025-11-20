namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

public record EntregaDelExamen
(
    Guid IdEstudiante,
    Guid IdExamen,
    DateTime FechaEntrega,
    string Respuesta,
    bool EntregadoFueraDeTiempo
);