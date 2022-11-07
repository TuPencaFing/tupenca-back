using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace tupenca_back.Model
{
    public class Puntaje
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int Resultado { get; set; }
        
        [Required]
        public int ResultadoExacto { get; set; }

    }
}

