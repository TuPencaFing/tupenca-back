using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class EstadisticaEventoDto
    {
        public decimal? PorcentajeEmpate { get; set; }

        public decimal? PorcentajeLocal { get; set; }

        public decimal? PorcentajeVisitante { get; set; }

    }
}
