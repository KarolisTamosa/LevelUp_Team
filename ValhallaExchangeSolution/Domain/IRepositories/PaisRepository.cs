using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IPaisRepository
    {
        Task<IEnumerable<Pais>> GetPais();
        Task<IEnumerable<Pais>> GetPaises();
        Task MeterPaises(List<Pais> Paises);
        Task<Pais> ObtenerPaisPorId(Guid IdPais);
        Task<IEnumerable<Pais>> GetPaisPorUsuario(Guid IdPais);

    }
}