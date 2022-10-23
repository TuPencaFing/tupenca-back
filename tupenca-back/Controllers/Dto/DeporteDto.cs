using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class DeporteDto
    {
        [Required]
        public string Nombre { get; set; }

    }
}
