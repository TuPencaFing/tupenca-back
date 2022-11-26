using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Model
{
    public class LookAndFeel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpresaId { get; set; }

        [Required]
        public string? Generaltext { get; set; }

        [Required]
        public string? Generalbackground { get; set; }

        [Required]
        public string? Textnavbar { get; set; }

        [Required]
        public string? Navbar { get; set; }

    }
}
