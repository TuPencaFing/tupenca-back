using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class PencaEmpresaDto
    {

        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public CampeonatoDto? Campeonato { get; set; }

        public List<PremioDto>? Premios { get; set; } = new List<PremioDto>();

        public int PuntajeId { get; set; }
        public virtual PuntajeDto? Puntaje { get; set; }

        public EmpresaDto? Empresa { get; set; }

    }
}

