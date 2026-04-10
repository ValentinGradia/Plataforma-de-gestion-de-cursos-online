namespace PlataformaDeGestionDeCursosOnline.Domain.DTOs;

public class UsuarioDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nombre { get; set; }
    public  string Apellido { get; set; }
}