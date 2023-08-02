using Datos;
using Negocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Conversor
    {
        public Conversor() { }
        public bool ComprobarNombre(string nombreEntrada, List<Divisa> lista)
        {
            return lista.Exists(objeto => objeto.Nombre == nombreEntrada);

        }
        public bool ComprobarImporte(double importe)
        {
            return importe >= 0;
        }

        public double Convertir(string nombreEntrada, string nombreSalida, double importe,List<Divisa> lista, List<HistorialMonedasPorUsuario> historial)
        {
            Divisa origen = lista.Where(moneda => moneda.Nombre.ToUpper().Equals(nombreEntrada.ToUpper())).FirstOrDefault();
            Divisa destino = lista.Where(moneda => moneda.Nombre.ToUpper().Equals(nombreSalida.ToUpper())).FirstOrDefault();
            var factor = (destino.ValorEnDolares/ origen.ValorEnDolares);
            var resultado = (double)factor * importe;
            GuardarEnHistorial(1, origen, destino, (double)factor, importe, resultado, historial);
            return resultado;
        }

        public void GuardarEnHistorial(int idUsuario, Divisa moneda1, Divisa moneda2, double factor, double importe, double resultado, List<HistorialMonedasPorUsuario> historial)
        {
            //ruta historial
            //c:
            //string rutaHistorial = Path.Combine("C:/archivos/historial", "historial.json");
            //string carpetaHistorial = @"C:/archivos/inbox";

            //string rutaHistorial = "historial.json";
            var registro = new HistorialMonedasPorUsuario()
            {
                IdUsuario = idUsuario,
                MonedaOrigen = moneda1,
                MonedaDestino = moneda2,
                Importe = importe,
                FactorCambio = factor,
                FechaConversion = DateTime.Now,
                Resultado = resultado
            };
            historial.Add(registro);
        }
    }
}
