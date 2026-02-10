namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;

public record Asistencia(
    Guid IdInscripcionEstudiante,
    bool Presente
);
