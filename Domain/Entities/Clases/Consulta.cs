namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Clases.ObjectValues;

using System;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;

public class Consulta : Entity
{
    public Guid IdClase { get; private set; }
    public Guid IdEstudiante { get; private set; }
    public string Titulo { get; private set; }
    public string Descripcion { get; private set; }
    public DateTime FechaConsulta { get; private set; }

    // Constructor para mapeo/serialización (p. ej. Dapper). Mantenerlo privado para uso interno.
    private Consulta() : base(){}


// Constructor para crear una nueva consulta desde el código de dominio
    public Consulta(Guid idClase, Guid idEstudiante, string titulo, string descripcion, DateTime fechaConsulta)
        : base(Guid.NewGuid())
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new ArgumentNullException(nameof(titulo));

        IdClase = idClase;
        IdEstudiante = idEstudiante;
        Titulo = titulo;
        Descripcion = descripcion;
        FechaConsulta = fechaConsulta;
    }

    // Constructor interno para reconstrucción desde la BBDD donde ya conocemos el Id
    internal Consulta(Guid id, Guid idClase, Guid idEstudiante, string titulo, string descripcion, DateTime fechaConsulta)
        : base(id)
    {
        IdClase = idClase;
        IdEstudiante = idEstudiante;
        Titulo = titulo;
        Descripcion = descripcion;
        FechaConsulta = fechaConsulta;
    }

    // Método de reconstrucción requerido (nombre: Reconstruir)
    public static Consulta Reconstruir(Guid id, Guid idClase, Guid idEstudiante, string titulo, string descripcion, DateTime fechaConsulta)
    {
        return new Consulta(id, idClase, idEstudiante, titulo, descripcion, fechaConsulta);
    }
}
