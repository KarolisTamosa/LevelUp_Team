namespace _6Trabajo2ConversorMonedasProyecto
{
    public class HistorialMonedasPorUsuario
    {
        public int IdUsuario { get; set; }
        public Moneda MonedaOrigen { get; set; }
        public Moneda MonedaDestino { get; set; }
        public double Importe { get; set; }
        public double FactorCambio { get; set; }
        public DateTime FechaConversion { get; set; }
        public double Resultado { get; set; }

        public HistorialMonedasPorUsuario(int idUsuario, Moneda monedaOrigen, Moneda monedaDestino, double importe, double factorCambio, DateTime fechaConversion, double resultado)
        {
            IdUsuario = idUsuario;
            MonedaOrigen = monedaOrigen;
            MonedaDestino = monedaDestino;
            Importe = importe;
            FactorCambio = factorCambio;
            FechaConversion = fechaConversion;
            Resultado = resultado;
        }

        public override string ToString()
        {
            return $"IdUsuario: {this.IdUsuario} - Moneda Origen: {this.MonedaOrigen} - Moneda Destino: {this.MonedaDestino} - " + 
                $"Importe: {this.Importe} - Factor Cambio: {this.FactorCambio} - FechaConversion: {this.FechaConversion} - Resultado: {this.Resultado}" ;
        }

    }
}
