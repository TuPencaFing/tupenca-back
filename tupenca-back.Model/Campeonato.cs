using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tupenca_back.Model
{
    public class Campeonato
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Image { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        [Required]
        public Deporte? Deporte { get; set; }
        public int ? DeporteId { get; set; } 

        public List<Evento> Eventos { get; set; } = new List<Evento>();

        public List<Penca> Pencas { get; set; } = new List<Penca>();

        public bool PremiosEntregados { get; set; } = false;

    }
}

