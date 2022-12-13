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

        public string? Image { get; set; }

        [Required]
        public Campeonato? Campeonato { get; set; }
        public int CampeonatoId { get; set; }

        public List<Premio>? Premios { get; set; } = new List<Premio>();

        [Required]
        public int PuntajeId { get; set; }
        public virtual Puntaje? Puntaje { get; set; }

        public List<UsuarioPenca>? UsuariosPencas { get; set; }

        public bool PremiosEntregados { get; set; } = false;
    }
}

