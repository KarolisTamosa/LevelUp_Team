namespace PropuestaEstructuraBBDD
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string PasswordEncriptado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdPais { get; set; }
    }
}