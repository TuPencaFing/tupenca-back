using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class EquipoDto
    {
        [Required]
        public string Nombre { get; set; }
    }
}
