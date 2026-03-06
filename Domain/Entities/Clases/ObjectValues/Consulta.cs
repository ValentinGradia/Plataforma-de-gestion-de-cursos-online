namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.ObjectValues;

public record Consulta(Guid IdClase,Guid IdEstudiante, string Titulo, string Descripcion, DateTime FechaConsulta);