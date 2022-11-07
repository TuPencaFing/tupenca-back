using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class EventoPrediccionDto
    {

        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        public Equipo EquipoLocal { get; set; }

        public Equipo EquipoVisitante { get; set; }

        public Prediccion? Prediccion { get; set; }

        public Resultado? Resultado { get; set; }

        public bool IsEmpateValid { get; set; } = true;

        public bool IsPuntajeEquipoValid { get; set; } = true;

    }
}
