using System.ComponentModel.DataAnnotations;

namespace Domain.Models{    
    public class HistorialPorUsuario
    {
        [Key]
        public int IdHistorialPorUsuario { get; set; }
        public int IdUsuario { get; set; }//FK
        public int IdMonedaOrigen { get; set; }//FK
        public int IdMonedaDestino { get; set; }//FK
        public int FactorCambio { get; set; } //guardamos el factor ya que cada dia los valores en dolares varian, los guardamos para hacer una simple operacion para calcular el resultado
        public double Importe { get; set; }
        public DateTime FechaConversion { get; set; }

    }
}