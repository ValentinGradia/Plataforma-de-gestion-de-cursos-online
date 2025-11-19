using System.Runtime.InteropServices.JavaScript;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Enums;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities;

public class Clase : Entity
{
    public string Material;
    public static Guid IdCurso { get; private set; }
    public DateTime Fecha { get; }
    private Asistencia asistenciaAlumno;
    
    //se van a mostrar en orden de cual fue la ultima 
    public Queue<string> consultasDeAlumnos;

    private Clase(string material) : base(Guid.NewGuid())
    {
        Material = material;
        Fecha = DateTime.UtcNow;
    }

    public void AsignarIdCurso(Guid idCurso)
    {
        Clase.IdCurso = idCurso;
    }
    
    public Clase IniciarClase(string material)

}