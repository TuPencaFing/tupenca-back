using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class EventoDto
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        [Required]
        public int EquipoLocalId { get; set; }

        [Required]
        public int EquipoVisitanteId { get; set; }
    }
}
