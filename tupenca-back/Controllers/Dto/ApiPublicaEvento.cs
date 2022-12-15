using System;
using System.ComponentModel.DataAnnotations.Schema;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
	public class ApiPublicaEventoDto
	{

        public string? Image { get; set; }

        public ApiPublicaEquipoDto? EquipoLocal { get; set; }

        public ApiPublicaEquipoDto? EquipoVisitante { get; set; }

    }
}

