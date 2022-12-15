using System;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
	public class ApiPublicaResultadoDto
	{

        public TipoResultado Resultado { get; set; }

        public int? PuntajeEquipoLocal { get; set; }

        public int? PuntajeEquipoVisitante { get; set; }

        public ApiPublicaEventoDto? Evento { get; set; }

    }
}

