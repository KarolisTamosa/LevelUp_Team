using Domain.IRepositories;
using Domain.IServices;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class HistorialService : IHistorialService
    {
        private readonly IHistorialRepository _historialRepository;
        public HistorialService(IHistorialRepository historialRepository)
        {
            _historialRepository = historialRepository;
        }
        public async Task<IEnumerable<Historial>> GetHistorialPorUsuario(Guid usuarioId)
        {
            return await _historialRepository.GetHistorialPorUsuario(usuarioId);
        }

    }
}
