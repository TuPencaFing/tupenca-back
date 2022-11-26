using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class LookAndFeelDto
    {
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
