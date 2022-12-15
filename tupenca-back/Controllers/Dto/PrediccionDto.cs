using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class PrediccionDto
    {
        public TipoResultado prediccion { get; set; }

        public int? PuntajeEquipoLocal { get; set; }

        public int? PuntajeEquipoVisitante { get; set; }

        public int? Score { get; set; }

    }
}
