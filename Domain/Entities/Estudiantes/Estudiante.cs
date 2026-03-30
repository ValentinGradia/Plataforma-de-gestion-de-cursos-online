using System.Collections.ObjectModel;
using PlataformaDeGestionDeCursosOnline.Domain.Abstractions;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.Enum;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Estudiantes;

public class Estudiante : Usuario 
{
    
    private List<Guid?> _cursosInscritosActualmente = new List<Guid?>();
    private List<Guid?> _historialDeCursos = new List<Guid?>();
    
    
    public ReadOnlyCollection<Guid?> CursosInscritosActualmente => _cursosInscritosActualmente.AsReadOnly();
    public ReadOnlyCollection<Guid?> HistorialDeCursos => _historialDeCursos.AsReadOnly();

    public Estudiante(string pais, string ciudad, string calle, int altura, string email, string contraseña, string dni, string nombre, string apellido) : base(pais, ciudad, calle, altura, email, contraseña, dni, nombre, apellido, Roles.Estudiante)
    {
        
    }

    // Constructor interno para reconstrucción desde BD
    internal Estudiante(Guid id, Direccion direccion, Email email, Contraseña contraseña, DNI dni, string nombre, string apellido, DateTime fechaRegistro) : base(id, direccion, email, contraseña, dni, nombre, apellido, fechaRegistro, Roles.Estudiante)
    {
    }
    
    public static Estudiante ReconstruirEstudiante(dynamic row)
    {
        Direccion direccion = Direccion.CrearDireccion(
            (string)row.Pais,
            (string)row.Ciudad,
            (string)row.Calle,
            (int)row.Altura
        );
        Email email           = Email.CrearEmail((string)row.Email);
        Contraseña contraseña = Contraseña.CrearContraseña((string)row.Contraseña);
        DNI dni               = DNI.CrearDNI((string)row.Dni);

        return new Estudiante(
            id:            (Guid)row.Id,
            direccion:     direccion,
            email:         email,
            contraseña:    contraseña,
            dni:           dni,
            nombre:        (string)row.Nombre,
            apellido:      (string)row.Apellido,
            fechaRegistro: (DateTime)row.FechaRegistro
        );
    }
    
    public void InscribirEnCurso(Guid cursoId)
    {
        if (_cursosInscritosActualmente.Contains(cursoId))
            throw new InvalidOperationException("Ya está inscrito en este curso.");

        _cursosInscritosActualmente.Add(cursoId);
    }
    
    public void DesinscribirDeCurso(Guid cursoId)
    {
        if (!_cursosInscritosActualmente.Contains(cursoId))
            throw new InvalidOperationException("No está inscrito en este curso.");

        _cursosInscritosActualmente.Remove(cursoId);
    }

    public void CompletarCurso(Guid cursoId)
    {
        _cursosInscritosActualmente.Remove(cursoId);
        _historialDeCursos.Add(cursoId);
    }

}