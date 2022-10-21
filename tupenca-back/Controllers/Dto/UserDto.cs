using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class UserDto
    {
        [Required]
        public string token { get; set; }
    }
}
