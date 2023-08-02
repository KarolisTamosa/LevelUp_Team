using Datos;
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

        public double Convertir(string nombreEntrada, string nombreSalida, double importe,List<Divisa> lista)
        {
            Divisa origen = lista.Where(moneda => moneda.Nombre.ToUpper().Equals(nombreEntrada)).FirstOrDefault();
            Divisa destino = lista.Where(moneda => moneda.Nombre.ToUpper().Equals(nombreSalida)).FirstOrDefault();
            var factor = (destino.ValorEnDolares/ origen.ValorEnDolares);
            var resultado = (double)factor * importe;
            return resultado;
        }
    }
}
