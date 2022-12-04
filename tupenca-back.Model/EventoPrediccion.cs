using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace tupenca_back.Model
{
    public class EventoPrediccion
    {
        public int Id { get; set; }

        public string? Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        public int EquipoLocalId { get; set; }
        public virtual Equipo EquipoLocal { get; set; }

        public int EquipoVisitanteId { get; set; }
        public virtual Equipo EquipoVisitante { get; set; }

        public bool IsEmpateValid { get; set; } = true;

        public bool IsPuntajeEquipoValid { get; set; } = true;

        public Prediccion? Prediccion { get; set; }

        public Resultado? Resultado { get; set; }

    }
}
