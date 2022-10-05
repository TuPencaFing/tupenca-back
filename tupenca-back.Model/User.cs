using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
