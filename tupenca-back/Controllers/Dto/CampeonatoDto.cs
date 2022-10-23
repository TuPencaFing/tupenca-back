using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class CampeonatoDto
    {
        
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime FinishDate { get; set; }

        [Required]
        public DeporteDto? Deporte { get; set; }

        public List<EventoDto> Eventos { get; set; } = new List<EventoDto>();

    }
}

