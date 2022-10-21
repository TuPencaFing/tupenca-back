using System;
using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public abstract class Penca
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Image { get; set; }

        [Required]
        public Campeonato? Campeonato { get; set; }

        [Required]
        public List<Premio>? Premios { get; set; } = new List<Premio>();

    }
}

