using System.Runtime.CompilerServices;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

public record Direccion
{
    public string Pais { get; private set; }
    public string Ciudad { get; private set; }
    public string Calle { get; private set; }
    public int Altura { get; private set; }

    private Direccion(string pais, string ciudad, string calle, int altura)
    {
        this.Pais = pais;
        this.Ciudad = ciudad;
        this.Calle = calle;
        this.Altura = altura;
    }

    public void CambiarPais(string pais)
    {
        this.Pais = pais;   
    }
    
    public void CambiarCalle(string calle)
    {
        this.Calle = calle;   
    }
    
    public void CambiarAltura(int altura)
    {
        this.Altura = altura;   
    }
    
    public void CambiarCiudad(string ciudad)
    {
        this.Ciudad = ciudad;   
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