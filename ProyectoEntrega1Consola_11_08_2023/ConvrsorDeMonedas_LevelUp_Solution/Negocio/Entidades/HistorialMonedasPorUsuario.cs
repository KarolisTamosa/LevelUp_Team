namespace Negocio
{
    public class HistorialMonedasPorUsuario
    {
        public int IdUsuario { get; set; }
        public Divisa MonedaOrigen { get; set; }
        public Divisa MonedaDestino { get; set; }
        public double Importe { get; set; }
        public double FactorCambio { get; set; }
        public DateTime FechaConversion { get; set; }
        public double Resultado { get; set; }

        public HistorialMonedasPorUsuario(){}
        public HistorialMonedasPorUsuario(int idUsuario, Divisa monedaOrigen, Divisa monedaDestino, double importe, double factorCambio, DateTime fechaConversion, double resultado)
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
        {//TODO Cambiar al estilo actual
            return $"Moneda Origen: {this.MonedaOrigen} - Moneda Destino: {this.MonedaDestino} - " +
                $"Importe: {this.Importe}\n - Factor Cambio: {this.FactorCambio} - FechaConversion: {this.FechaConversion} - Resultado: {this.Resultado}";
        }
    }
}
