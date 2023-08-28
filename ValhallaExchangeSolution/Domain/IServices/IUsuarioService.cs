using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IUsuarioService
    { 
        Task<Usuario> GetUsuarioPorID(Guid id);
        Task<bool> ActualizarUsuario(Usuario usuario);
        Task<bool> ExisteUsuarioConEmailIndicado(string email);
        Task CrearUsuario(Usuario usuario);
        Task<Usuario> ValidarUsuarioParaLogueo(Usuario usuario);
    }
}
