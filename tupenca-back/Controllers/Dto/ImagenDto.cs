using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class ImagenDto
    {
        [Required]
        public IFormFile file { get; set; }
    }
}
