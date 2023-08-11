using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string PasswordEncriptado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdPais { get; set; }
        public bool Eliminado { get; set; }
    }
}