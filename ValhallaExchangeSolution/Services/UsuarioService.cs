using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        { 
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Usuario> GetUsuarioPorID(Guid id)
        {
            return await _usuarioRepository.GetUsuarioPorID(id);
        }
    }
}
