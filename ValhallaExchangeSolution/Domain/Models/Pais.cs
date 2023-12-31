﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Pais
    {
        [Key]
        public Guid IdPais { get; set; }
        public string Nombre { get; set; }
        public bool Eliminado { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}