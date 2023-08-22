using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Historial
{
    public class HistorialGetDTO
    {

        public Guid IdHistorialPorUsuario { get; set; }
        public Guid IdUsuario { get; set; }
        public string NombreMonedaOrigen { get; set; }
        public double ValorMonedaOrigen { get; set; }
        public string NombreMonedaDestino { get; set; }
        public double ValorMonedaDestino { get; set; }
        public double Importe { get; set; }
        public DateTime FechaConversion { get; set; }
        public double ResultadoConversion { get; set; }

    }
}
