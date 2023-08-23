using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IHistorialRepository
    {
        Task<IEnumerable<Historial>> GetHistorialPorUsuario(Guid usuarioId);
        Task GuardarRegistroDeHistorial(Historial historial);
        Task<Historial> GetHistorialById(Guid idHistorial);
        Task BorrarRegistroDeHistorial(Historial historial);

    }
}
