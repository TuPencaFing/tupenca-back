using System.ComponentModel.DataAnnotations;

namespace tupenca_back.Controllers.Dto
{
    public class EmpresaDto
    {

        [Required]
        [Display(Name = "Razon Social")]
        public string Razonsocial { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*", ErrorMessage = "Debe contener unicamente numeros.")]
        [MaxLength(12)]
        [MinLength(12)]
        public string RUT { get; set; }
    }
}
