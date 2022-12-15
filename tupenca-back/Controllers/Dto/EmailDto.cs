using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class EmailDto
    {

        [EmailAddress]
        public string Email { get; set; }
    }
}
