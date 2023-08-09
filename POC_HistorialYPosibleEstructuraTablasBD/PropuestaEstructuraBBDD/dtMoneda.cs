namespace PropuestaEstructuraBBDD
{
    public class dtMoneda
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string? Nombre { get; set; }
        public double ValorEnDolares { get; set; }
        public bool Eliminado { get; set; }
    }
}