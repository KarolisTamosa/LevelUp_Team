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
        Task MeterPaises(List<Pais> Paises);
        Task<Moneda> ObtenerPaisPorId(Guid IdPais);
    }
}