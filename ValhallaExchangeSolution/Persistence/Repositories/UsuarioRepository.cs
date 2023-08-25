using Domain.IRepositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    { 
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> GetUsuarioPorID(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _context.Usuarios.Where(usuario => usuario.IdUsuario.Equals(id) && !usuario.Eliminado).FirstOrDefaultAsync();
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            //_context.Entry(usuario).State = EntityState.Modified;
            //await _context.SaveChangesAsync();


            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            // Busca el usuario en la base de datos por su ID
            var usuarioExistente = await GetUsuarioPorID(usuario.IdUsuario);

            if (usuarioExistente == null)
            {
                return false; // O puedes lanzar una excepción si lo prefieres
            }

            // Actualiza las propiedades del usuario existente con las del nuevo usuario
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.PasswordEncriptado = usuario.PasswordEncriptado;
            // Actualiza otras propiedades según sea necesario

            // Guarda los cambios en la base de datos
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> ExisteUsuarioConEmailIndicado(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            return await _context.Usuarios.AnyAsync(usuario => usuario.Email.ToUpper().Equals(email.ToUpper()) && !usuario.Eliminado);
        }

        public async Task CrearUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));//para que no entre a la bbdd y no la bloquee
            }
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }
        //devuelve null si no existe un usuario con las credenciales
        public async Task<Usuario> ValidarUsuarioParaLogueo(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));//para que no entre a la bbdd y no la bloquee
            }
            if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.PasswordEncriptado))
            {
                throw new ArgumentNullException(nameof(string.Empty));//para que no entre a la bbdd y no la bloquee
            }
            //hacer consulta en bbdd con username y password
            var user = await _context.Usuarios.Where(u => u.Email == usuario.Email && u.PasswordEncriptado == usuario.PasswordEncriptado).FirstOrDefaultAsync();
            return user;
        }
    }
}
