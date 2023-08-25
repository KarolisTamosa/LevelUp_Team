
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
    }
}
