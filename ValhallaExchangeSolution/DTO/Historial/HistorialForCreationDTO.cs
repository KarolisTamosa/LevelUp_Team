﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Historial
{
    public class HistorialForCreationDTO
    {
        public string CodigoMonedaOrigen { get; set; }
        public string CodigoMonedaDestino { get; set; }
        public double Importe { get; set; }
        public double ResultadoConversion { get; set; }
    }
}
