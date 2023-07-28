namespace PropuestaEstructuraBBDD
{
    public class HistorialPorUsuario
    {
        //moneda origen
        //moneda destino
        //factor conversion
        //cantidad
        //resultado
        //fecha de conversion
        //usuario
        public int IdHistorialPorUsuario { get; set; }
        public int IdUsuario { get; set; }
        // a traves de la eleccion de la moneda origen y moneda destino buscar el id del factor de conversion
        public int IdFactorConversion { get; set; }
        public double Importe { get; set; }
        public DateTime FechaConversion { get; set; }

    }
}