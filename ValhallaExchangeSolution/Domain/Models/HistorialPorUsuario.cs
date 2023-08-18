using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models{    
    public class HistorialPorUsuario
    {
        [Key]
        public int IdHistorialPorUsuario { get; set; }


        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }//FK
        public Usuario Usuario { get; set; }


        [ForeignKey("MonedaOrigen")]
        [InverseProperty("HistorialesPorUsuarioOrigen")]
        public int IdMonedaOrigen { get; set; }//FK
        public Moneda MonedaOrigen { get; set; }


        [ForeignKey("MonedaDestino")]
        [InverseProperty("HistorialesPorUsuarioDestino")]
        public int IdMonedaDestino { get; set; }//FK
        public Moneda MonedaDestino { get; set; }

        public int FactorCambio { get; set; } //guardamos el factor ya que cada dia los valores en dolares varian, los guardamos para hacer una simple operacion para calcular el resultado
        public double Importe { get; set; }
        public DateTime FechaConversion { get; set; }

    }
}