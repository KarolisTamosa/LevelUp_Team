using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Usuario
{
    public class UsuarioDTO
    {
        public Guid IdUsuario { get; set; }
        public string Email { get; set; }
        public Guid IdPais { get; set; }//FK
        public int Edad { get; set; }
    }
}
