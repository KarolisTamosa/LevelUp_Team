using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class UsuarioUtils
    {
        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Today;
            int edad = fechaActual.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > fechaActual.AddYears(-edad))
            {
                edad--;
            }
            return edad;
        }


        public static bool EsEdadPermitida(DateTime fechaNacimiento)
        {
            return CalcularEdad(fechaNacimiento) >= 18;
        }
        
        public static bool EsPasswordConFormatoValido(string password, int longitudMinima, int longitudMaxima)
        {
            //TODO: Mas adelante podemos verificar que tenga una mayuscula mas un caracter especial
            int longitud = password.Length;
            return longitud >= longitudMinima && longitud <= longitudMaxima;
        }

        public static bool EsEmailConFormatoValido(string email)
        {
            //TODO: Mas adelante comprobar que tiene el formato de "@gmail.com"
            return true;
        }
    }
}
