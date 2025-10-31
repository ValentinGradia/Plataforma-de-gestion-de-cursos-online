using System.Runtime.CompilerServices;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

public record Direccion
{
    public string Pais { get; }
    public string Ciudad { get; }
    public string Calle { get; }
    public int Altura { get; }

    private Direccion(string pais, string ciudad, string calle, int altura)
    {
        this.Pais = pais;
        this.Ciudad = ciudad;
        this.Calle = calle;
        this.Altura = altura;
    }

    //static factory method -> separar responasbilidades (el constructor solo crea el objeto sin validar)
    public static Direccion CrearDireccion(string pais, string ciudad, string calle, int altura)
    {
        if (string.IsNullOrEmpty(pais))
            throw new ArgumentNullException(nameof(pais));

        if (string.IsNullOrEmpty(ciudad))
            throw new ArgumentNullException(nameof(ciudad));

        if (string.IsNullOrEmpty(calle))
            throw new ArgumentNullException(nameof(calle));

        if (altura <= 0)
            throw new ArgumentOutOfRangeException(nameof(altura));
        
        return new Direccion(pais, ciudad, calle, altura);
    }
}