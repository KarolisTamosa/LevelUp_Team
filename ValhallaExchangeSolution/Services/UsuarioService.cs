using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;

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

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            return await _usuarioRepository.ActualizarUsuario(usuario);
        }

        public async Task<bool> ExisteUsuarioConEmailIndicado(string email)
        {
            return await _usuarioRepository.ExisteUsuarioConEmailIndicado(email);
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            await _usuarioRepository.CrearUsuario(usuario);
        }

        public async Task<Usuario> ValidarUsuarioParaLogueo(Usuario usuario)
        {
            return await _usuarioRepository.ValidarUsuarioParaLogueo(usuario);
        }
    }
}
