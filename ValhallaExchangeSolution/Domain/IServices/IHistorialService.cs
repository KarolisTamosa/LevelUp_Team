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
    }
}
