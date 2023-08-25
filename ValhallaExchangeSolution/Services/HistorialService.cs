using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Services
{
    public class HistorialService : IHistorialService
    {
        private readonly IHistorialRepository _historialRepository;
        public HistorialService(IHistorialRepository historialRepository)
        {
            _historialRepository = historialRepository;
        }

        public async Task<IEnumerable<Historial>> GetHistorialPorUsuario(Guid usuarioId, int numResultados)
        {
            return await _historialRepository.GetHistorialPorUsuario(usuarioId, numResultados);
        }

        public async Task GuardarRegistroDeHistorial(Historial historial)
        {
            await _historialRepository.GuardarRegistroDeHistorial(historial);
        }
        public async Task BorrarRegistroDeHistorial(Historial historial)
        {
            await _historialRepository.BorrarRegistroDeHistorial(historial);
        }

        public async Task<Historial> GetHistorialById(Guid idHistorial)
        {
            return await _historialRepository.GetHistorialById(idHistorial);
        }

        public async Task<IEnumerable<Historial>> GetHistorialPorUsuarioConProcedimientoAlmacenado(Guid usuarioId, int numResultados)
        {
            return await _historialRepository.GetHistorialPorUsuarioConProcedimientoAlmacenado(usuarioId, numResultados);
        }

        public async Task<Historial> GetHistorialByIdHistorialEIdUsuario(Guid idHistorial, Guid idUsuario)
        {
            return await _historialRepository.GetHistorialByIdHistorialEIdUsuario(idHistorial, idUsuario);
        }
    }
}
