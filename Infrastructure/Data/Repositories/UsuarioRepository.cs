using System.Data;
using Dapper;
using PlataformaDeGestionDeCursosOnline.Application.DTOs;
using PlataformaDeGestionDeCursosOnline.Domain;
using PlataformaDeGestionDeCursosOnline.Domain.Entities;
using PlataformaDeGestionDeCursosOnline.Domain.Entities.Usuarios;
using PlataformaDeGestionDeCursosOnline.Domain.GlobalInterfaces;

namespace PlataformaDeGestionDeCursosOnline.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private IUsuarioRepository _usuarioRepositoryImplementation;

    public UsuarioRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task GuardarAsync(Usuario objeto)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = @"INSERT INTO Usuarios 
                        (Id, Nombre, Apellido, Email, Contraseña, Dni, Rol, Pais, Ciudad, Calle, Altura, FechaRegistro) 
                    VALUES 
                        (@Id, @Nombre, @Apellido, @Email, @Contrasena, @Dni, @Rol, @Pais, @Ciudad, @Calle, @Altura, @FechaRegistro)";

        await connection.ExecuteAsync(sql, new
        {
            Id           = objeto.Id,
            Nombre       = objeto.Nombre,
            Apellido     = objeto.Apellido,
            Email        = objeto.Email.valorEmail,
            Contraseña   = objeto.Contraseña.ValorContraseña,
            Dni          = objeto.Dni.Valor,
            Rol          = objeto.Rol.ToString(),
            Pais         = objeto.Direccion.Pais,
            Ciudad       = objeto.Direccion.Ciudad,
            Calle        = objeto.Direccion.Calle,
            Altura       = objeto.Direccion.Altura,
            FechaRegistro = objeto.FechaRegistro
        });
    }

    public async Task ActualizarAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email WHERE Id = @Id";
        await connection.ExecuteAsync(new CommandDefinition(sql, new { Id = usuario.Id, Nombre = usuario.Nombre, Apellido = usuario.Apellido, Email = usuario.Email.valorEmail}, cancellationToken: cancellationToken));
    }
    

    public async Task<Usuario?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Usuarios WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Usuario>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }

    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT * FROM Usuarios";
        return await connection.QueryAsync<Usuario>(sql);
    }

    public async Task<UsuarioDTO?> VerDatosUsuario(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = _connectionFactory.CreateConnection();
        var sql = "SELECT Id, Email, Nombre, Apellido FROM Usuarios WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<UsuarioDTO>(new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken));
    }
}
