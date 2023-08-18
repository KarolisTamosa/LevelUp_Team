using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string PasswordEncriptado { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Eliminado { get; set; }
        
        
        [ForeignKey("Pais")]
        public int IdPais { get; set; }//FK
        public Pais Pais { get; set; }

        public IEnumerable<HistorialPorUsuario> HistorialesPorUsuario { get; set; }

    }
}