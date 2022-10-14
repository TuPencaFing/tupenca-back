using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class Deporte
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }


    }
}
