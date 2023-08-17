using System.ComponentModel.DataAnnotations;

namespace Domain.Models{

    public class Moneda
    {
        [Key]
        public int IdMoneda { get; set; }
        //[MaxLength(3)]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double ValorEnDolares { get; set; }
        public bool Eliminado { get; set; }
    }
}