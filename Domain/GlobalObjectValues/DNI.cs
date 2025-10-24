using System.Text.RegularExpressions;

namespace PlataformaDeGestionDeCursosOnline.Domain.GlobalObjectValues;

public record DNI
{
    public string Valor { get; }

    private DNI(string valor) => this.Valor = valor;

    public static DNI CrearDNI(string valor)
    {
        if (valor is null)
        {
            throw new ArgumentNullException(nameof(valor), "DNI no puede ser nulo.");
        }

        
        string dniLimpio = Regex.Replace(valor.Trim(), @"[^\d]", "");

        //validamos que sea solo de 8 caracteres
        if (dniLimpio.Length != 8)
        {
            throw new ArgumentException("DNI debe contener 8 caracteres.", nameof(valor));
        }

        if (!(int.TryParse(valor, out _)))
        {
            throw new ArgumentException(("DNI solo debe contener caracteres numericos."), nameof(valor));
        }
        
        return new DNI(dniLimpio);
    }
};