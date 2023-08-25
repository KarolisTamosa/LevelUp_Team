using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Usuario
{
    public class UsuarioParaActualizarDTO
    {
        public string Email { get; set; }
        public string PasswordEncriptado { get; set; }
    }
}
