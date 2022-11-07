using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class PayerDto
    {
        public string email { get; set; }

        public IdentificationDto identification { get; set; }

    }
}
