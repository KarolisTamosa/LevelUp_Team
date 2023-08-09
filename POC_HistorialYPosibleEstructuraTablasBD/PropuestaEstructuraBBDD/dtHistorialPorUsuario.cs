using System.ComponentModel.DataAnnotations;

namespace PropuestaEstructuraBBDD
{
    //public class HistorialPorUsuario
    //{
    //    //moneda origen
    //    //moneda destino
    //    //factor conversion
    //    //cantidad
    //    //resultado
    //    //fecha de conversion
    //    //usuario
    //    public int IdHistorialPorUsuario { get; set; }
    //    public int IdUsuario { get; set; }
    //    // a traves de la eleccion de la moneda origen y moneda destino buscar el id del factor de conversion
    //    public int IdFactorConversion { get; set; }
    //    public double Importe { get; set; }
    //    public DateTime FechaConversion { get; set; }

    //}
    public class dtHistorialPorUsuario
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