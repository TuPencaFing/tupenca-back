using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Equipo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Nombre { get; set; }

    }
}
