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
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository _paisService;

        public PaisService(IPaisRepository paisRepository)
        {
            _paisService = paisRepository;
        }

        public Task<IEnumerable<Pais>> GetPaises()
        {
            return _paisService.GetPaises();
        }

        public Task<Pais> GetPaisPorId(Guid IdPais)
        {
            return _paisService.ObtenerPaisPorId(IdPais);
        }

        public async Task<IEnumerable<Pais>> GetPaisPorUsuario(Guid IdPais)
        {
            return await _paisService.GetPaisPorUsuario(IdPais);
        }
    }
}