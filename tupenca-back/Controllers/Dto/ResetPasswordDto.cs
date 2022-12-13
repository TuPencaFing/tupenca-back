using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class ResetPasswordDto
    {

        public string token { get; set; }

        public string password { get; set; }

    }
}
