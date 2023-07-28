namespace _6Trabajo2ConversorMonedasProyecto
{
    public enum Moneda
    {
        Euros,
        Dolares,
        Libras
    }

    public class ConversorMonedas
    {
        public static double ConvertirMoneda(Moneda monedaOrigen, Moneda monedaDestino, double importe)
        {
            //dolares -> euros
            if (monedaOrigen == Moneda.Dolares && monedaDestino == Moneda.Euros)
            {
                return importe * 0.89;
            }
            //euros -> dolares
            if (monedaOrigen == Moneda.Euros && monedaDestino == Moneda.Dolares)
            {
                return importe * 1.12;
            }
            //dolares -> libras
            if (monedaOrigen == Moneda.Dolares && monedaDestino == Moneda.Libras)
            {
                return importe * 0.77;
            }
            //libras -> dolares
            if (monedaOrigen == Moneda.Libras && monedaDestino == Moneda.Dolares)
            {
                return importe * 1.3;
            }
            //euros -> libras
            if (monedaOrigen == Moneda.Euros && monedaDestino == Moneda.Libras)
            {
                return importe * 0.87;
            }
            //libras -> euros
            if (monedaOrigen == Moneda.Libras && monedaDestino == Moneda.Euros)
            {
                return importe * 1.15;
            }
            return importe;
        }
    }
}
