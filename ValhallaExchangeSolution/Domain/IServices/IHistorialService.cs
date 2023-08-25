using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IServices
{
    public interface IHistorialService
    {
        Task<IEnumerable<Historial>> GetHistorialPorUsuario(Guid usuarioId);
        Task GuardarRegistroDeHistorial(Historial historial);
        Task<Historial> GetHistorialById(Guid idHistorial);
        Task BorrarRegistroDeHistorial(Historial historial);
        Task<Historial> GetHistorialByIdHistorialEIdUsuario(Guid idHistorial, Guid idUsuario);
    }
}
