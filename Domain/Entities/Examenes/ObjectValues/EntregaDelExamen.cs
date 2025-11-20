using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Examenes.ObjectValues;

public record EntregaDelExamen
(
    Guid IdEstudiante,
    Guid IdExamen,
    DateTime FechaEntrega,
    TipoExamen tipo,
    string Respuesta,
    bool EntregadoFueraDeTiempo,
    EstadoEntregaDelExamen estadoEntrega
);