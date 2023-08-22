using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models{    
    public class Historial
    {
        [Key]
        public Guid IdHistorialPorUsuario { get; set; }


        [ForeignKey("Usuario")]
        public Guid IdUsuario { get; set; }//FK
        public Usuario Usuario { get; set; }


        [ForeignKey("MonedaOrigen")]
        public Guid IdMonedaOrigen { get; set; }//FK
        public Moneda MonedaOrigen { get; set; }


        [ForeignKey("MonedaDestino")]
        
        public Guid IdMonedaDestino { get; set; }//FK
        public Moneda MonedaDestino { get; set; }

        public int FactorCambio { get; set; } //guardamos el factor ya que cada dia los valores en dolares varian, los guardamos para hacer una simple operacion para calcular el resultado
        public double Importe { get; set; }
        public DateTime FechaConversion { get; set; }

        public double ResultadoConversion { get; set; }
        public bool Eliminado { get; set; }

    }
}