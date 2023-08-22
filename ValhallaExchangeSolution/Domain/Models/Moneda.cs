using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models{

    public class Moneda
    {
        [Key]
        public Guid IdMoneda { get; set; }
        //[MaxLength(3)]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public double ValorEnDolares { get; set; }
        public bool Eliminado { get; set; }
        public IEnumerable<HistorialPorUsuario> HistorialesPorMonedaOrigen { get; set; }
        public IEnumerable<HistorialPorUsuario> HistorialesPorMonedaDestino { get; set; }
    }
}