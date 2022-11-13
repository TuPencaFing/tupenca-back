using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;

namespace tupenca_back.Controllers.Dto
{
    public class EmpresaPaymentDto
    {

        public int Id { get; set; }

        public string? Image { get; set; }

        [Display(Name = "Razon Social")]
        public string? Razonsocial { get; set; }

        [RegularExpression(@"^[0-9]*", ErrorMessage = "Debe contener unicamente numeros.")]
        [MaxLength(12)]
        [MinLength(12)]
        public string? RUT { get; set; }

        public int PlanId { get; set; }

        public PagosTarjetaDto Pago { get; set; }

    }
}
