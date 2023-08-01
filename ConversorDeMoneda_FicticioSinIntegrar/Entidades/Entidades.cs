namespace Entidades
{
    public class Moneda
    {
        public Moneda(int id, double valor, string nombre)
        {
            Id = id;
            Valor=valor;
            Nombre=nombre;
        }
        public int Id { get; set; }
        public double Valor { get; set; }
        public string Nombre { get; set; }
        public override string ToString()
        {
            return String.Concat(Id+"."+Nombre+"("+Valor+")");
        }
    }


}