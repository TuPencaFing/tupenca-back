using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class EventoFechaDto
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
    }
}
