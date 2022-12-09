using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class EventoDto
    {

        public int Id { get; set; }

        public string? Image { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        public int EquipoLocalId { get; set; }

        public int EquipoVisitanteId { get; set; }

        public EquipoDto? EquipoLocal { get; set; }

        public EquipoDto? EquipoVisitante { get; set; }

        public bool IsEmpateValid { get; set; }

        public bool IsPuntajeEquipoValid { get; set; }

    }
}
