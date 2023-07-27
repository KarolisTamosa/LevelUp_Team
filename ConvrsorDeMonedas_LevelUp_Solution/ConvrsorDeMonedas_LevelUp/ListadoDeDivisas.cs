//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//
//namespace ConvrsorDeMonedas_LevelUp
//{
//    public class Divisa
//    {
//        public string Nombre { get; set; }
//        public double ValorEnDolares { get; set; }
//    }
//
//    public class ListadoDeDivisas
//    {
//        public List<Divisa> Divisas { get; private set; }
//
//        public ListadoDeDivisas()
//        {
//            Divisas = new List<Divisa>();
//            AgregarDivisa("Euro", 1.18);
//            AgregarDivisa("Yen Japonés", 0.0090);
//            AgregarDivisa("Libra Esterlina", 1.39);
//            AgregarDivisa("Franco Suizo", 1.08);
//            AgregarDivisa("Dólar Canadiense", 0.80);
//            AgregarDivisa("Dólar Australiano", 0.74);
//            AgregarDivisa("Yuan Chino", 0.15);
//            AgregarDivisa("Dólar de Hong Kong", 0.13);
//            AgregarDivisa("Corona Sueca", 0.12);
//            AgregarDivisa("Peso Mexicano", 0.050);
//        }
//
//        private void AgregarDivisa(string nombre, double valorEnDolares)
//        {
//            Divisas.Add(new Divisa { Nombre = nombre, ValorEnDolares = valorEnDolares });
//        }
//    }
//
//}
