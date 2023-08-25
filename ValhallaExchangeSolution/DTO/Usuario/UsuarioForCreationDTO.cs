using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.Usuario
{
    public class UsuarioForCreationDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public Guid IdPais { get; set; }
    }
}
