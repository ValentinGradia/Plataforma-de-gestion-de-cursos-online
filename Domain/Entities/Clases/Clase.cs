using System.Runtime.InteropServices.JavaScript;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities;

public class Clase : Entity
{
    public string Material;
    public Guid IdCurso { get; }
    public DateTime Fecha { get; }
    private Asistencia asistenciaAlumno;

    public Clase(string material, Guid idCurso) : base(Guid.NewGuid())
    {
        Material = material;
        IdCurso = idCurso;
        Fecha = DateTime.UtcNow;
    }

}