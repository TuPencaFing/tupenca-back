using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class EquipoDto
    {

        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

    }
}
