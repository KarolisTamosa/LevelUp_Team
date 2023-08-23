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
        private readonly PaisService _paisService;

        public PaisService(IPaisRepository paisRepository)
        {
            _paisService = (PaisService?)paisRepository;
        }

        public async Task<IEnumerable<Pais>> GetPaisPorUsuario(Guid IdPais)
        {
            return await _paisService.GetPaisPorUsuario(IdPais);
        }
    }
}