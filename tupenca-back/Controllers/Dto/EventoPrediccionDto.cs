using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class EventoPrediccionDto
    {

        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        public EquipoDto EquipoLocal { get; set; }

        public EquipoDto EquipoVisitante { get; set; }

        public PrediccionDto? Prediccion { get; set; }

        public ResultadoDto? Resultado { get; set; }

    }
}
